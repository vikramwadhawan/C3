using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Security.Model
{
    public class AuthState
    {
        public string stateKey { get; set; }

        public string authStatus { get; set; }

        public string userId { get; set; }

        public string console { get; set; }
        public string message { get; set; }

    }
}
