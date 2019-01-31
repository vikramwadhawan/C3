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
    public class ADGManager
    {
        public static async Task<IEnumerable<ADGAgentModel>> GetADGActivity(bool notCompleted = true)
        {
            List<ADGAgentModel> activities = new List<ADGAgentModel>();
            var result = await ColligoO365RMOManager<VwADGAgent>.FindAsync(x => x.IsProcessed == !notCompleted, false);
            //converT data to framework model
            if (result != null && result.Any())
            {
                Type source = typeof(VwADGAgent);
                Type destination = typeof(ADGAgentModel);
                foreach (var item in result)
                {
                    ADGAgentModel obj = new ADGAgentModel();
                    CopyHelper.Copy(source, item, destination, obj);
                    activities.Add(obj);
                }
            }
            return activities;
        }

        public static async Task<string> SaveADGAgent(ADGAgentModel model)
        {
            string result = "success";
            if (model != null)
            {
                ADGAgent data = await ColligoO365RMOManager<ADGAgent>.FirstOrDefaultAsync(p => p.ADGAgentId == model.ADGAgentId);
                if (data != null)
                {
                    data.IsProcessed = model.IsProcessed;
                    data.ProcessStatus = model.ProcessStatus;
                    data.ProcessDescription = model.ProcessDescription;
                    data.ModifiedBy = model.CreatedBy;
                    data.ModifiedOn = DateTime.Now.ToUniversalTime();
                    int count = await ColligoO365RMOManager<ADGAgent>.SaveAsync(null, data);
                    if (count == 0)
                    {
                        result = "fail";
                    }
                }
                else
                    result = "fail";
            }
            return result;
        }
    }
}
