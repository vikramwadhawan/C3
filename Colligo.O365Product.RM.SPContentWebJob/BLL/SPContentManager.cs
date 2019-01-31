using Colligo.O365Product.APIConstant;
using Colligo.O365Product.HttpUtility.ServiceHelper;
using Colligo.O365Product.Model;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.SPContentWebJob.BLL
{
    public class SPContentManager : HttpClientHelper
    {
        public SPContentManager(string url, ILogManager logManager) : base(logManager)
        {
            ServiceUrl = url;
        }

        public IEnumerable<SpContentLogModel> GetAllChangeLog(ContentLogRequest request)
        {
            bool error;
            return GetManyByPostFromAPI<ContentLogRequest, SpContentLogModel>(request, SPService.GetAllContentLog, out error, setTimeout: true);
        }
    }
}
