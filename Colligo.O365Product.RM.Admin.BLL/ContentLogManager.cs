using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Admin.DAL;
using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.BLL
{
    public class ContentLogManager
    {
        public static async Task<string> SaveContentLog(List<SpContentLogModel> logs, long eventSettingId)
        {
            string result = "success";
            if (logs != null && logs.Count > 0)
            {
                List<ContentLog> contentLogs = new List<ContentLog>();
                Type source = typeof(SpContentLogModel);
                Type destination = typeof(ContentLog);
                foreach (var item in logs)
                {
                    ContentLog obj = new ContentLog();
                    CopyHelper.Copy(source, item, destination, obj);
                    obj.ModifiedBy = obj.CreatedBy;
                    obj.ModifiedOn = DateTime.Now.ToUniversalTime();
                    contentLogs.Add(obj);
                }
                int count = await ColligoO365RMOManager<ContentLog>.SaveAsync(contentLogs);
                if (count > 0)
                {
                    //update settings with last execution date
                    result = await EventSettingManager.UpdateEventSetting(eventSettingId, logs[0].CreatedBy, logs[0].ExecutionTime, true);

                    //filter data for ADG process
                    List<ADGAgent> agent = new List<ADGAgent>();
                    Type agentsource = typeof(ADGAgent);
                    foreach (var item in contentLogs)
                    {
                        if (!string.IsNullOrEmpty(item.ComplianceTag) && item.EventColumnValue != null)
                        {
                            ADGAgent obj = new ADGAgent();
                            CopyHelper.Copy(destination, item, agentsource, obj);
                            obj.EventColumnValue = item.EventColumnValue.Value;
                            obj.IsProcessed = false;
                            agent.Add(obj);
                            //agent.Add(new ADGAgent()
                            //{
                            //    ContentLogId = item.ContentLogId,
                            //     ComplainceAssetId=item.ComplainceAssetId,
                            //      ComplianceTag=item.ComplianceTag,
                            //       ContentType=item.ContentType 
                            //});
                        }
                    }
                    if (agent.Count > 0)
                    {
                        count = await ColligoO365RMOManager<ADGAgent>.SaveAsync(agent);
                    }
                }
            }
            return result;
        }
    }
}
