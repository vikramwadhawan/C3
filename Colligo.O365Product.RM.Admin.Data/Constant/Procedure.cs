using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.Data.Constant
{
    public static class Procedure
    {
        public static readonly string UspReportADGAgent = "exec UspReportADGAgent {0},{1},{2},{3}";
        public static readonly string UspReportGetContentLog = "exec UspReportGetContentLog {0},{1},{2},{3},{4},{5}";
        public static readonly string UspReportChartData = "exec UspReportChartData {0},{1},{2}";
        public static readonly string UspReportContentAuditSummary = "exec UspReportContentAuditSummary {0},{1},{2},{3}";
        public static readonly string UspEventSettingGetMappingCount = "exec UspEventSettingGetMappingCount {0}";
    }
}
