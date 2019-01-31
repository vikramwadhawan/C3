using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ServiceInterface
{
    public interface IAPIService
    {
        string ApiUrl { get; set; }

        string ApiToken { get; set; }

        string GetAuthToken(string id_token, string userAgent);
    }
}
