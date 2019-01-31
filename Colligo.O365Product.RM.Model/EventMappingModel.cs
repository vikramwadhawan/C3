using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public partial class EventMappingModel
    {
        public int MappedCount { get; set; }
        public string OrganizationRootUrl { get; set; }
        public string ContentSiteUrl { get; set; }
    }
}
