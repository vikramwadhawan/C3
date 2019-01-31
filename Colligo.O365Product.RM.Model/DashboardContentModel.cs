using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class DashboardContentModel
    {
        public int Id { get; set; }
        public Nullable<int> RowNumber { get; set; }
        public long ContentLogId { get; set; }
        public string ContentType { get; set; }
        public string DocId { get; set; }
        public System.DateTime LastModifiedTime { get; set; }
        public string FileType { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> EventColumnValue { get; set; }
        public string ComplianceTag { get; set; }
        public string ComplianceTagId { get; set; }
        public string ComplainceAssetId { get; set; }
        public string DocUrl { get; set; }
        public string LibraryName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string DocumentName { get; set; }
        public string ErrorLog { get; set; }
    }
}
