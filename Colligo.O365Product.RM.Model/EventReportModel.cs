using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class EventReportModel
    {
        public int ResultId { get; set; }
        public long ItemProcessedToday { get; set; }
        public long ItemProcessedToDate { get; set; }
        public long ItemErrorsToDate { get; set; }
        public long EventCreatedToday { get; set; }
        public long EventUpdatedToday { get; set; }
        public long EventCreatedToDate { get; set; }
        public long TotalActiveContentType { get; set; }
    }
}
