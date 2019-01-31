using Colligo.O365Product.Helper.Extensions;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.Model;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.SPContentWebJob.BLL
{
    public static class SpContentJobHandler
    {
        private static ILogManager _logger;
        static ConcurrentDictionary<long, long> spContentRetrivalActivities = new ConcurrentDictionary<long, long>();
        public static SpContentJobManager JobManager;
        public static SPContentManager SPServiceManager;
        public static void SetProperties(ILogManager logger, SpContentJobManager jobManager, SPContentManager spServiceManager)
        {
            _logger = logger;
            JobManager = jobManager;
            SPServiceManager = spServiceManager;
        }

        /// <summary>
        /// This method will update metadata and move file from source to destination ..it will move uploaded files. basedOn applicationId..
        /// </summary>
        public static async Task HandleSpContentJob()
        {
            try
            {
                var result = JobManager.GetAllEventSetting();
                if (result != null && result.Count > 0)
                {
                    List<EventSettingModel> notProcessed = null;
                    if (spContentRetrivalActivities.Count > 0)
                    {
                        List<long> existingKeys = spContentRetrivalActivities.GetKeys();
                        notProcessed = result.Where(p => !existingKeys.Contains(p.EventSettingId)).ToList();
                    }
                    else
                    {
                        notProcessed = result.ToList();
                    }

                    if (notProcessed != null && notProcessed.Count() > 0)
                    {
                        foreach (var item in notProcessed)
                        {
                            long id = item.EventSettingId;
                            spContentRetrivalActivities.TryAdd(id, id);
                        }
                        List<Task> task = new List<Task>();
                        //group entries for organization IDs
                        var results = notProcessed.GroupBy(
                              p => p.OrganizationMasterId,
                              (key, g) => new { sourceName = key, activity = g.ToList() });
                        foreach (var item in results)
                        {
                            task.Add(ExecuteActivity(item.activity));
                        }
                        await Task.WhenAll(task);
                    }
                }
            }
            catch (Exception ex) { _logger.Error("HandleSpContentJob error", ex); }
            await Task.Delay(1);
        }

        private static void RemoveFromProcess(long eventSettingId)
        {
            long id;
            if (!spContentRetrivalActivities.TryRemove(eventSettingId, out id))
                spContentRetrivalActivities.TryRemove(eventSettingId, out id);
        }

        private static async Task ExecuteActivity(List<EventSettingModel> appActivities)
        {
            bool removeProess = false;
            if (appActivities != null)
            {
                string timezone = appActivities[0].TimeZone;
                string time = appActivities[0].Time;
                if (!string.IsNullOrEmpty(timezone) && !string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(appActivities[0].UserId) && !string.IsNullOrEmpty(appActivities[0].Password))
                {
                    try
                    {
                        DateTime currentTime = DateTime.Now;
                        if (SpContentJobManager.IsValidExecutionTime(timezone, time, Convert.ToInt32(SpContentJobConstant.SpContentIntervalInMinute), out currentTime))
                        {
                            _logger.Info($"excecuting setup for {appActivities[0].OrganizationRootUrl} at {currentTime}");
                            List<Task> task = new List<Task>();
                            foreach (var appActivity in appActivities)
                            {
                                long id = appActivity.EventSettingId;
                                if (spContentRetrivalActivities.TryGetValue(id, out id))
                                    task.Add(ExecuteActivity(appActivity));
                            }
                            await Task.WhenAll(task);
                        }
                        else
                        {
                            removeProess = true;
                            _logger.Info($"{currentTime} is not setup for {appActivities[0].OrganizationRootUrl}");
                        }
                    }
                    catch (Exception ex)
                    {
                        removeProess = true;
                        _logger.Error($"failed to ExecuteActivity for {appActivities[0].OrganizationRootUrl}", ex);
                    }
                }
                else
                {
                    removeProess = true;
                    _logger.Info($"Either Time/timezone or user/pwd is not setup for {appActivities[0].OrganizationRootUrl}");
                }
            }
            if (removeProess)
            {
                foreach (var item in appActivities)
                {
                    RemoveFromProcess(item.EventSettingId);
                }
            }
            await Task.Delay(0);
        }

        private static async Task ExecuteActivity(EventSettingModel appActivity)
        {
            long eventSettingId = appActivity.EventSettingId;
            try
            {
                string result = "success";
                ContentLogRequest request = GenerateContentRequest(appActivity);
                DateTime currentExecutionDate = DateTime.Now.ToUniversalTime();
                IEnumerable<SpContentLogModel> logs = SPServiceManager.GetAllChangeLog(request);
                List<ADGAgentModel> agentDetail = new List<ADGAgentModel>();
                if (logs != null && logs.Count() > 0)
                {
                    foreach (var log in logs)
                    {
                        log.IsProcessed = true;
                        log.OrganizationMasterId = appActivity.OrganizationMasterId;
                        log.CreatedBy = 1;//TODO
                        log.CreatedOn = DateTime.Now.ToUniversalTime();
                        log.ExecutionTime = currentExecutionDate;
                        log.ContentType = appActivity.ContentType;
                    }
                    result = JobManager.SaveContentLog(logs.ToList(), appActivity.EventSettingId);

                }
                else
                {
                    _logger.Info($"not get any content logs for  {appActivity.OrganizationRootUrl} for content type {appActivity.EventColumnName} at-{currentExecutionDate.ToString("MM/dd/yyyy hh:mm:ss")} ");
                }
                bool sendMail = false;

                if (result != "success")
                {
                    sendMail = true;
                }
                RemoveFromProcess(eventSettingId);
                if (sendMail)
                {
                    //var emailResult = await SendEmail(uploadDetails, result);
                    //if (emailResult.ToLower() != "success")
                    //    _logger.Error($"failed to send email for metadata update for {uploadDetails.BlobModels[0].NewName }");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to process content log for {eventSettingId}", ex);
                RemoveFromProcess(eventSettingId);
            }
            finally
            {
                await Task.Delay(0);
            }
        }

        private static ContentLogRequest GenerateContentRequest(EventSettingModel appActivity)
        {
            return new ContentLogRequest()
            {
                ContentTypeId = appActivity.ContentTypeId,
                EventDateColumn = appActivity.EventColumnInternalName,
                Password = EncryptionHelper.Decrypt(appActivity.Password, SpContentJobConstant.EncryptionKey),
                UserId = appActivity.UserId,
                SiteUrl = appActivity.OrganizationRootUrl,
                ModifiedDate = appActivity.LastRetrivalTime.HasValue ? appActivity.LastRetrivalTime.Value.AddHours(-1) : DateTime.MinValue.Date
            };
        }
    }
}
