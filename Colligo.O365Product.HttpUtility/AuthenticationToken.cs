

using System;
using System.Runtime.Serialization;

namespace Colligo.O365Product.HttpUtility
{
    [Serializable]
    [DataContract]
    public class AuthenticationToken
    {
        [DataMember]
        public string access_token;
        [DataMember]
        public string token_type;
        [DataMember]
        public int expires_in;
        [DataMember]
        public string error;
    }
}
