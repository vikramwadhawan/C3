using Colligo.O365Product.RM.Admin.DAL;
using Colligo.O365Product.RM.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.BLL
{
    public static class ApplicationManager
    {
        /// <summary>
        /// Return all application messages
        /// </summary>
        /// <param name="includeDeActive"></param>
        /// <returns></returns>
        public static List<Tuple<string, string>> GetAllErrorMessage(bool includeDeActive = false)
        {
            List<Tuple<string, string>> errorMessage = new List<Tuple<string, string>>();
            List<ErrorMessage> message = ColligoO365RMOManager<ErrorMessage>.Find(p => p.IsActive || includeDeActive).ToList();
            if (message != null && message.Count > 0)
            {
                errorMessage = message.Select(item => new Tuple<string, string>(item.ErrorKey, item.Message)).ToList();
            }
            return errorMessage;
        }

        /// <summary>
        /// return errormessage key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetErrorMessageByKey(string key)
        {
            var result = ColligoO365RMOManager<ErrorMessage>.FirstOrDefault(p => ((p.ErrorKey == key)));
            return result == null ? string.Empty : result.Message;
        }

    }
}
