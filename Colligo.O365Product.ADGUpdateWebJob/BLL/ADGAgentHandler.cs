using Colligo.O365Product.EmailService;
using Colligo.O365Product.Helper.Extensions;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.ScriptRunner;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ADGUpdateWebJob.BLL
{
    public class ADGAgentHandler
    {
        private static ILogManager _logger;
        static ConcurrentDictionary<string, long> adgAgentdataActivities = new ConcurrentDictionary<string, long>();
        public static ADGJobManager JobManager;

        public static void SetProperties(ILogManager logger, ADGJobManager jobManager)
        {
            _logger = logger;
            JobManager = jobManager;
        }

        /// <summary>
        /// This method will update metadata and move file from source to destination ..it will move uploaded files. basedOn applicationId..
        /// </summary>
        public static async Task HandleADGAgentActivity()
        {
            try
            {
                var result = JobManager.GetAllADGAgentActivity();
                if (result != null && result.Count > 0)
                {
                    List<ADGAgentModel> notProcessed = null;
                    if (adgAgentdataActivities.Count > 0)
                    {
                        List<string> existingKeys = adgAgentdataActivities.GetKeys();
                        notProcessed = result.Where(p => !existingKeys.Contains(p.ADGProcessId)).ToList();
                    }
                    else
                    {
                        notProcessed = result.ToList();
                    }

                    if (notProcessed != null && notProcessed.Count() > 0)
                    {
                        _logger.Info($"get adg setup records to process{notProcessed.Count()} ");
                        // add all entries to not processed dictionary.
                        foreach (var item in notProcessed)
                        {
                            long id = item.ADGAgentId;
                            string value = item.ADGProcessId;
                            adgAgentdataActivities.TryAdd(value, id);
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
                    else
                    {
                        _logger.Info($"not get any adg setup to create at- {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")} ");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HandleADGAgentActivity error", ex);
            }
            await Task.Delay(1);
        }

        private static async Task ExecuteActivity(List<ADGAgentModel> appActivities)
        {
            if (appActivities != null)
            {
                bool removeProess = false;
                string userId = appActivities[0].UserId;
                string password = EncryptionHelper.Decrypt(appActivities[0].Password, ADGUpdateConstant.EncryptionKey);
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    PowerShellHelper scriptExecuter = null;
                    try
                    {
                        scriptExecuter = new PowerShellHelper();
                        scriptExecuter.OpenSpace();
                        if (scriptExecuter.StartNewSession(ADGUpdateConstant.Office365SecurityCenter, userId, password))
                        {
                            foreach (var appActivity in appActivities)
                            {
                                long id = appActivity.ADGAgentId;
                                string value = appActivity.ADGProcessId;
                                if (adgAgentdataActivities.TryGetValue(value, out id))
                                    await ExecuteActivity(appActivity, scriptExecuter);
                            }
                        }
                        else
                        {
                            removeProess = true;
                            _logger.Info($"failed to connect using powershell for {appActivities[0].UserId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        removeProess = true;
                        _logger.Error($"failed to ExecuteActivity for {appActivities[0].OrganizationMasterId}", ex);
                    }
                    finally
                    {
                        scriptExecuter.EndSession();
                        scriptExecuter.CloseSpace();
                    }
                }
                else
                {
                    removeProess = true;
                    _logger.Info($"UID/ Pwd not setup for {appActivities[0].OrganizationMasterId}");
                }
                if (removeProess)
                {
                    foreach (var item in appActivities)
                    {
                        RemoveFromProcess(item.ADGProcessId);
                    }
                }
            }
            await Task.Delay(0);
        }

        private static async Task ExecuteActivity(ADGAgentModel appActivity, PowerShellHelper scriptExecuter)
        {
            long adgAgentId = appActivity.ADGAgentId;
            string processId = appActivity.ADGProcessId;
            try
            {
                string result = "success";
                bool sendMail = false;
                string eventTypeId = GetEventTypeIdByComplianceTag(scriptExecuter, appActivity.ComplianceTag);
                if (!string.IsNullOrEmpty(eventTypeId))
                {
                    bool createEvent = true;
                    string eventName = GenerateEventName(appActivity);
                    bool isDuplicate = false;
                    bool isUpdated = false;
                    //remove existing
                    if (GetComplianceRetentionEvent(scriptExecuter, eventName, appActivity.EventColumnValue, out isDuplicate))
                    {
                        createEvent = false;
                        if (isDuplicate)
                        {
                            appActivity.ProcessStatus = ADGProcessStatus.Duplicate;
                            appActivity.ProcessDescription = $"Event name - {eventName} and error is exist in the system";

                        }
                        else
                        {
                            if (RemoveComplianceRetentionEvent(scriptExecuter, eventName))
                            {
                                isUpdated = true;
                                createEvent = true;
                            }
                            else
                            {
                                appActivity.ProcessStatus = ADGProcessStatus.Error;
                                appActivity.ProcessDescription = $"Failed to remove existing event name - {eventName} and error is {scriptExecuter.GetError()}";
                            }
                        }
                    }
                    if (createEvent)
                    {
                        string assestId = !string.IsNullOrEmpty(appActivity.ComplainceAssetId) ? appActivity.ComplainceAssetId : appActivity.DocId;
                        if (CreateComplianceRetentionEvent(scriptExecuter, eventName, appActivity.EventColumnValue.ToString("MM/dd/yyyy h:mm tt"), eventTypeId, assestId))
                        {
                            appActivity.ProcessStatus = isUpdated ? ADGProcessStatus.Updated : ADGProcessStatus.Created;
                            appActivity.ProcessDescription = eventName;
                        }
                        else
                        {
                            appActivity.ProcessStatus = ADGProcessStatus.Error;
                            appActivity.ProcessDescription = $"Failed to remove existing event name - {eventName} and error is {scriptExecuter.GetError() }";
                        }
                    }
                    appActivity.IsProcessed = true;
                    result = await JobManager.UpdateADGAgentActivity(appActivity);
                }
                else
                {
                    appActivity.ProcessStatus = ADGProcessStatus.Error;
                    appActivity.ProcessDescription = $"Event Type Id is missing for Complinace tag - {appActivity.ComplianceTag}";
                    appActivity.IsProcessed = true;
                    result = await JobManager.UpdateADGAgentActivity(appActivity);
                }
                if (result != "success")
                {
                    sendMail = true;
                }
                RemoveFromProcess(processId);
                if (sendMail)
                {
                    //var emailResult = await SendEmail(uploadDetails, result);
                    //if (emailResult.ToLower() != "success")
                    //    _logger.Error($"failed to send email for metadata update for {uploadDetails.BlobModels[0].NewName }");
                }
            }
            catch (Exception ex)
            {
                appActivity.IsProcessed = true;
                appActivity.ProcessStatus = ADGProcessStatus.Error;
                appActivity.ProcessDescription = ex.ToString();
                await JobManager.UpdateADGAgentActivity(appActivity);
                _logger.Error($"Failed to process ExecuteActivity for {adgAgentId}", ex);
                RemoveFromProcess(processId);
            }
            finally
            {
                await Task.Delay(1);
            }
        }

        private static string GenerateEventName(ADGAgentModel appActivity)
        {
            string name = string.Empty;
            name = $"{appActivity.ComplianceTag} {(string.IsNullOrEmpty(appActivity.ComplainceAssetId) ? appActivity.DocId : appActivity.ComplainceAssetId) }";
            if (name.Length > 62)
                name = name.Substring(0, 62);
            return name;
        }

        private static string GetEventTypeIdByComplianceTag(PowerShellHelper scriptExecuter, string tagName)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(tagName))
            {
                string tagScript = ADGUpdateConstant.GetComplianceTag;
                Collection<PSObject> scriptResult = scriptExecuter.ExecuteScriptBlock(tagScript.Replace("{Tag}", tagName));
                if (scriptResult != null && scriptResult.Count > 0)
                {
                    result = Convert.ToString(scriptResult[0].Properties.FirstOrDefault(p => p.Name == "EventTypeId")?.Value) ?? string.Empty;
                }
            }
            return result;
        }

        private static bool CreateComplianceRetentionEvent(PowerShellHelper scriptExecuter, string eventName, string eventDate, string eventTypeId, string assestId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(eventName))
            {
                string tagScript = ADGUpdateConstant.NewComplianceRetentionEvent;
                tagScript = tagScript.Replace("{EventName}", eventName).Replace("{EventDate}", eventDate).Replace("{EventTypeId}", eventTypeId).Replace("{AssetId}", assestId).Replace("{SharePointAssetIdQuery}", assestId);
                Collection<PSObject> scriptResult = scriptExecuter.ExecuteScriptBlock(tagScript);
                result = scriptResult.Count == 1;
            }
            return result;
        }

        private static bool GetComplianceRetentionEvent(PowerShellHelper scriptExecuter, string eventName, DateTime eventColumnValue, out bool isDuplicate)
        {
            bool result = false;
            isDuplicate = false; ;
            if (!string.IsNullOrEmpty(eventName))
            {
                string tagScript = ADGUpdateConstant.GetComplianceRetentionEvent;
                tagScript = tagScript.Replace("{EventName}", eventName);
                Collection<PSObject> scriptResult = scriptExecuter.ExecuteScriptBlock(tagScript);
                result = scriptResult.Count == 1;
                if (result)
                {
                    string columnValue = Convert.ToString(scriptResult[0].Properties.FirstOrDefault(p => p.Name == "EventDateTime")?.Value) ?? string.Empty;
                    if (!string.IsNullOrEmpty(columnValue))
                    {
                        DateTime dtEventValue = DateTime.Now;
                        if (DateTime.TryParse(columnValue,out dtEventValue))
                        {
                            isDuplicate = (dtEventValue == eventColumnValue);
                        }
                    }
                }
            }
            return result;
        }

        private static bool RemoveComplianceRetentionEvent(PowerShellHelper scriptExecuter, string eventName)
        {
            bool result = false;
            int count = scriptExecuter.GetCount();
            if (!string.IsNullOrEmpty(eventName))
            {
                string tagScript = ADGUpdateConstant.RemoveComplianceRetentionEvent;
                tagScript = tagScript.Replace("{EventName}", eventName);
                Collection<PSObject> scriptResult = scriptExecuter.ExecuteScriptBlock(tagScript);
                result = (count == scriptExecuter.GetCount());
            }
            return result;
        }

        private static void RemoveFromProcess(string value)
        {
            long id;
            if (!adgAgentdataActivities.TryRemove(value, out id))
                adgAgentdataActivities.TryRemove(value, out id);
        }

        //private static async Task<string> SendEmail(UploadRequest request, string errorMessage)
        //{
        //    EmailService service = new SendGridEmailService(ADGUpdateConstant.SendGridKey);
        //    service.To.Add(request.UserEmailAddress);
        //    service.FromAddress = ADGUpdateConstant.FromAddress;
        //    service.Subject = ADGUpdateConstant.Subject;
        //   // service.Body = CommmonLogic.ConvertToEmailString(request, errorMessage);
        //    return await service.SendEmailAsync();
        //}
    }
}
