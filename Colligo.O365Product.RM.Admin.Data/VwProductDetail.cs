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
    
    public partial class VwProductDetail
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsProductActive { get; set; }
        public int LicenseCount { get; set; }
        public Nullable<int> LicenseUsed { get; set; }
        public int OrganizationMasterId { get; set; }
        public Nullable<int> SystemUserId { get; set; }
        public Nullable<bool> IsProductUserActive { get; set; }
    }
}
