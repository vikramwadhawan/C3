using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class EventSummaryModel
    {
        public int OrganizationMasterId { get; set; }
        public Nullable<int> ResultSeries { get; set; }
        public int ItemProcessed { get; set; }
        public int ItemFailed { get; set; }
        public int EventCreated { get; set; }
        public string ReportTimeline { get; set; }
    }
}
