using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class EventSettingUserModel
    {
        public string SiteUrl { get; set; }
        public string SpUserId { get; set; }
        public string SpUserPassword { get; set; }
        public string AdgUserId { get; set; }
        public string AdgUserPassword { get; set; }
       public AgentExecutionTimeModel agentTimeZoneModel { get; set; }

    }
}
