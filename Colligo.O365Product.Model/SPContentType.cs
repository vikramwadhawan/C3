using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Model
{
    public class SPContentType
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        List<SharePointField> ContentColumns { get; set; }
    }
}
