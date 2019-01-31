using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Helper
{
    public class JsonHelper
    {
        public static T ConvertToObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string ConvertToJson<T>(T entity)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(entity, settings);
        }
    }
}
