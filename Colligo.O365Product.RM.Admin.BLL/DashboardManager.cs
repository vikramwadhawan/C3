using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Admin.DAL;
using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Admin.Data.Constant;
using Colligo.O365Product.RM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.BLL
{
    public class DashboardManager
    {
        public static async Task<EventReportModel> GetEventReport(DashboardSearchModel searchModel)
        {
            EventReportModel report = null;
            if (searchModel != null)
            {
                object[] param = new object[] { searchModel.OrganizationUrl, searchModel.StartDate, searchModel.EndDate, searchModel.Status };
                var result = await ColligoO365RMOManager<DashboardResult>.ExecuteSqlQueryAsync(Procedure.UspReportADGAgent, param);
                //converT data to framework model
                if (result != null && result.Any())
                {
                    Type source = typeof(DashboardResult);
                    Type destination = typeof(EventReportModel);
                    foreach (var item in result)
                    {
                        report = new EventReportModel();
                        CopyHelper.Copy(source, item, destination, report);
                        break;
                    }
                }
            }
            return report;
        }

        public static async Task<IEnumerable< DashboardContentModel>> GetContentLog(DashboardSearchModel searchModel)
        {
            List<DashboardContentModel> modelResult = new List<DashboardContentModel>();
            DashboardContentModel report = null;
            if (searchModel != null)
            {
                object[] param = new object[] { searchModel.OrganizationUrl, searchModel.StartDate, searchModel.EndDate, searchModel.ErrorRecord, searchModel.PageNumber, searchModel.PageSize };
                var result = await ColligoO365RMOManager<DashboardContentLog>.ExecuteSqlQueryAsync(Procedure.UspReportGetContentLog, param);
                //converT data to framework model
                if (result != null && result.Any())
                {
                    Type source = typeof(DashboardContentLog);
                    Type destination = typeof(DashboardContentModel);
                    foreach (var item in result)
                    {
                        report = new DashboardContentModel();
                        CopyHelper.Copy(source, item, destination, report);
                        modelResult.Add(report);
                    }
                }
            }
            return modelResult;
        }

        public static async Task<IEnumerable<EventSummaryModel>> GetEventSummary(DashboardSearchModel searchModel)
        {
            List<EventSummaryModel> modelResult = new List<EventSummaryModel>();
            EventSummaryModel report = null;
            if (searchModel != null)
            {
                object[] param = new object[] { searchModel.OrganizationUrl, searchModel.StartDate, searchModel.EndDate };
                var result = await ColligoO365RMOManager<DashboardChartData>.ExecuteSqlQueryAsync(Procedure.UspReportChartData, param);
                //converT data to framework model
                if (result != null && result.Any())
                {
                    Type source = typeof(DashboardChartData);
                    Type destination = typeof(EventSummaryModel);
                    foreach (var item in result)
                    {
                        report = new EventSummaryModel();
                        CopyHelper.Copy(source, item, destination, report);
                        modelResult.Add(report);
                    }
                }
            }
            return modelResult;
        }

        public static async Task<IEnumerable<ContentLogSummaryModel>> GetContentAuditSummary(DashboardSearchModel searchModel)
        {
            List<ContentLogSummaryModel> modelResult = new List<ContentLogSummaryModel>();
            ContentLogSummaryModel report = null;
            if (searchModel != null)
            {
                object[] param = new object[] { searchModel.OrganizationUrl, searchModel.SearchTerm, searchModel.PageNumber, searchModel.PageSize };
                var result = await ColligoO365RMOManager<ContentAuditSummary>.ExecuteSqlQueryAsync(Procedure.UspReportContentAuditSummary, param);
                //converT data to framework model
                if (result != null && result.Any())
                {
                    Type source = typeof(ContentAuditSummary);
                    Type destination = typeof(ContentLogSummaryModel);
                    foreach (var item in result)
                    {
                        report = new ContentLogSummaryModel();
                        CopyHelper.Copy(source, item, destination, report);
                        modelResult.Add(report);
                    }
                }
            }
            return modelResult;
        }
    }
}
