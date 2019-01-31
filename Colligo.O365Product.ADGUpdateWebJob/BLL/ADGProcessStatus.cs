using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ADGUpdateWebJob.BLL
{
    public static class ADGProcessStatus
    {
        public static readonly string Created = "Created";
        public static readonly string Updated = "Updated";
        public static readonly string Error = "Error";
        public static readonly string Duplicate = "Duplicate";
    }
}
