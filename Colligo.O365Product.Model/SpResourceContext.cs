using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Model
{

    [Serializable]
    [DataContract]
    public class SpResourceContext
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Host { get; set; }
        [DataMember]
        public string AccessToken { get; set; }
        [DataMember]
        public string RefreshToken { get; set; }
        [DataMember]
        public string ExpiresOn { get; set; }
        [DataMember]
        public bool ExtendedLifeTimeToken { get; set; }
    }
}
