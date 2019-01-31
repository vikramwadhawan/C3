using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.HttpUtility
{
    public class WebApiResult<T> where T : class
    {

        #region fileds
        private bool hasError;
        private T data;
        private List<string> errorMessages;
        #endregion
        #region Memebers
        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        public List<string> ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }
        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }
        #endregion
    }
}
