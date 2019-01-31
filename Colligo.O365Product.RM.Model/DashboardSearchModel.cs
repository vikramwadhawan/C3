using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class DashboardSearchModel
    {
        public string OrganizationUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string SearchTerm { get; set; }
        public bool ErrorRecord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
