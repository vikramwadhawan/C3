using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Model
{
    public class SharepointRequest
    {
        public string Url { get; set; }
        public string AccessToken { get; set; }
        public string ServerRelativeUrl { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string FolderName { get; set; }
        public string SearchTerm { get; set; }
        public string ContentTypeId { get; set; }
        public string FieldId { get; set; }
        public string Referer { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string IdToken { get; set; }
        public bool FolderExist { get; set; }
        public string ClientTimeZone { get; set; }
        public string PagingInfo { get; set; }
    }
}
