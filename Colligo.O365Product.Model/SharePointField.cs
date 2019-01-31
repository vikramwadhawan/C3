using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Model
{
    public class SharePointField
    {
        public string FieldId { get; set; }
        public string ContentTypeId { get; set; }
        public string ContentTypeName { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }
        public string TypeAsStr { get; set; }
        public string InternalName { get; set; }
        public string Required { get; set; }
        public string Value { get; set; }
        public bool IsSharedField { get; set; }
    }
}
