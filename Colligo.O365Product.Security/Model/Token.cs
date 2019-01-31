using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Security.Model
{
    [Serializable]
    [DataContract]
    public class Token
    {
        [DataMember]
        public string resource { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string id_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public double expires_in { get; set; }
        [DataMember]
        public string expires_on { get; set; }
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string suberror { get; set; }
        //, not_before, scope, pwd_exp, pwd_url
    }
}
