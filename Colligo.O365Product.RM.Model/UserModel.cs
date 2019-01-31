using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    [Serializable]
    [DataContract]
    public class UserModel
    {
        [DataMember]
        public int OrganizationMasterId { get; set; }
        [DataMember]
        public string OrganizationName { get; set; }
        [DataMember]
        public string OrganizationId { get; set; }
        [DataMember]
        public int SystemUserId { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public bool IsOrganizationActive { get; set; }
        [DataMember]
        public bool IsUserActive { get; set; }
        [DataMember]
        public string OrganizationRootUrl { get; set; }
    }
}
