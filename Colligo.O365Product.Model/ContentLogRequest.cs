using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Model
{
    public class ContentLogRequest
    {
        public string SiteUrl { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EventDateColumn { get; set; }
        public string ContentTypeId { get; set; }
    }
}
