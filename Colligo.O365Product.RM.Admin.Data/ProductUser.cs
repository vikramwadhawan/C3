//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Colligo.O365Product.RM.Admin.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductUser
    {
        public int ProductUserId { get; set; }
        public int ProductId { get; set; }
        public int OrganizationMasterId { get; set; }
        public int SystemUserId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
