using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colligo.O365Product.Helper;
using Colligo.O365Product.Helper.Helper;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.Model;
using Microsoft.SharePoint.Client.CompliancePolicy;
namespace Colligo.O365Product.SharePointData
{
    public class SharepointDataManager
    {
        public static string siteContextUrl = String.Empty;
        public static ClientContext ctx = null;
        public static async Task<List<SpContentLogModel>> GetModifiedContentInSharepoint(ContentLogRequest request)
        {
            List<SpContentLogModel> models = new List<SpContentLogModel>();
            int i = 0;
            bool contentExist = true;
            string eventDate = request.EventDateColumn;
            try
            {
                using (var clientContext = GetClientContext(request))
                {
                    var tags = GetAllTagsForSite(clientContext);
                    KeywordQuery keywordQuery = new KeywordQuery(clientContext);
                    keywordQuery.ClientType = "ContentSearchRegular";
                    do
                    {
                        ClientResult<ResultTableCollection> results = SearchContentLog(request, i, eventDate, clientContext, keywordQuery);
                        if (results != null && results.Value.Count > 0)
                        {
                            foreach (var resultRow in results.Value[0].ResultRows)
                            {
                                ConvertToContentModel(request, models, eventDate, tags, resultRow);
                            }
                        }
                        if (results.Value[0].ResultRows.Count() < 500)
                        {
                            contentExist = false;
                        }
                        else
                        {
                            i++;
                        }

                    } while (contentExist == true);
                    await Task.Delay(1);
                    return models;

                }
            }
            catch { throw; }
        }

        private static void ConvertToContentModel(ContentLogRequest request, List<SpContentLogModel> models, string eventDate, IEnumerable<ComplianceTagModel> tags, IDictionary<string, object> resultRow)
        {
            SpContentLogModel contentModel = new SpContentLogModel();
            var modifiedDate = Convert.ToDateTime(resultRow["ModifiedOWSDATE"]).ToUniversalTime();
            if (modifiedDate >= request.ModifiedDate)
            {

                contentModel.Author = !string.IsNullOrEmpty(Convert.ToString(resultRow["Author"])) ? resultRow["Author"].ToString() : "";
                contentModel.ComplianceTag = !string.IsNullOrEmpty(Convert.ToString(resultRow["ComplianceTag"])) ? resultRow["ComplianceTag"].ToString() : "";
                contentModel.ComplainceAssetId = !string.IsNullOrEmpty(Convert.ToString(resultRow["ComplianceAssetId"])) ? resultRow["ComplianceAssetId"].ToString() : "";
                if (!string.IsNullOrEmpty(contentModel.ComplianceTag))
                {
                    contentModel.ComplianceTagId = tags.FirstOrDefault(v => v.TagName == contentModel.ComplianceTag.ToString())?.TagId ?? "";
                }
                contentModel.DocId = !string.IsNullOrEmpty(Convert.ToString(resultRow["DocId"])) ? resultRow["DocId"].ToString() : "";
                contentModel.DocumentName = !string.IsNullOrEmpty(Convert.ToString(resultRow["FileName"])) ? resultRow["FileName"].ToString() : "";
                contentModel.DocUrl = !string.IsNullOrEmpty(Convert.ToString(resultRow["DefaultEncodingURL"])) ? resultRow["DefaultEncodingURL"].ToString() : "";
                contentModel.FileType = !string.IsNullOrEmpty(Convert.ToString(resultRow["FileType"])) ? resultRow["FileType"].ToString() : "";
                contentModel.LastModifiedTime = modifiedDate;
                contentModel.Title = !string.IsNullOrEmpty(Convert.ToString(resultRow["Title"])) ? resultRow["Title"].ToString() : "";
                contentModel.ContentTypeId = !string.IsNullOrEmpty(Convert.ToString(request.ContentTypeId)) ? request.ContentTypeId.ToString() : "";
                contentModel.SpWebUrl = !string.IsNullOrEmpty(Convert.ToString(resultRow["SPWebUrl"])) ? resultRow["SPWebUrl"].ToString() : "";
                GetEventColumnValue(request, eventDate, resultRow, contentModel,tags);
                models.Add(contentModel);
            }
        }

        private static void GetEventColumnValue(ContentLogRequest request, string eventDate, IDictionary<string, object> resultRow, SpContentLogModel contentModel, IEnumerable<ComplianceTagModel> labels)
        {
            
            ctx = GetWebContext(contentModel.SpWebUrl, request, ctx);
            using (ctx)
            {
                var listId = !string.IsNullOrEmpty(Convert.ToString(resultRow["ListId"])) ? resultRow["ListId"].ToString() : "";
                List list = ctx.Web.Lists.GetById(listId.ToGuid());
                ctx.Load(list);
                ctx.ExecuteQuery();
                string listItemId = !string.IsNullOrEmpty(Convert.ToString(resultRow["ListItemID"])) ? resultRow["ListItemID"].ToString() : "";
                ListItem listItem = list.GetItemById(listItemId);
                ctx.Load(listItem);
                ctx.ExecuteQuery();
                contentModel.LibraryName = list.Title;
                contentModel.EventColumnValue = !string.IsNullOrEmpty(Convert.ToString(listItem[eventDate])) ? Convert.ToDateTime(listItem[eventDate]) : (DateTime?)null;

                if (string.IsNullOrEmpty(Convert.ToString(resultRow["ComplianceTag"])))
                {
                    contentModel.ComplianceTag = !string.IsNullOrEmpty(Convert.ToString(listItem["_ComplianceTag"])) ? listItem["_ComplianceTag"].ToString() : "";
                    contentModel.ComplianceTagId = labels.FirstOrDefault(v => v.TagName == contentModel.ComplianceTag.ToString())?.TagId ?? "";
                    contentModel.ComplainceAssetId = !string.IsNullOrEmpty(Convert.ToString(listItem["ComplianceAssetId"])) ? listItem["ComplianceAssetId"].ToString() : "";
                }
                if (!string.IsNullOrEmpty(contentModel.ComplianceTag) && string.IsNullOrEmpty(Convert.ToString(resultRow["ComplianceAssetId"])))
                {
                    listItem["ComplianceAssetId"] = !string.IsNullOrEmpty(Convert.ToString(resultRow["DocId"])) ? resultRow["DocId"].ToString() : "";
                    listItem.SystemUpdate();
                    ctx.ExecuteQuery();
                    contentModel.ComplainceAssetId = !string.IsNullOrEmpty(Convert.ToString(resultRow["DocId"])) ? resultRow["DocId"].ToString() : "";
                }

            }
        }

        private static ClientResult<ResultTableCollection> SearchContentLog(ContentLogRequest request, int i, string eventDate, ClientContext clientContext, KeywordQuery keywordQuery)
        {
            keywordQuery.QueryText = "(Site:" + request.SiteUrl + ") AND ((ContentTypeId:" + request.ContentTypeId + "*))";
            StringCollection selectProperties = keywordQuery.SelectProperties;
            keywordQuery.RowLimit = 500;
            keywordQuery.RowsPerPage = 500;
            keywordQuery.Timeout = 10000;
            keywordQuery.StartRow = i;
            keywordQuery.SelectProperties.Add("SPSiteUrl");
            keywordQuery.SelectProperties.Add("FileName");
            keywordQuery.SelectProperties.Add("ListItemID");
            keywordQuery.SelectProperties.Add("Author");
            keywordQuery.SelectProperties.Add("DocId");
            keywordQuery.SelectProperties.Add("ComplianceTag");
            keywordQuery.SelectProperties.Add("ComplianceAssetId");
            keywordQuery.SelectProperties.Add("Path");
            keywordQuery.SelectProperties.Add("FileType");
            keywordQuery.SelectProperties.Add("ModifiedOWSDATE");
            keywordQuery.SelectProperties.Add("ListUrl");
            keywordQuery.SelectProperties.Add("ListId");
            keywordQuery.SelectProperties.Add("SPWebUrl");
            keywordQuery.SelectProperties.Add("DefaultEncodingURL");
            keywordQuery.SortList.Add("SPSiteUrl", SortDirection.Descending);
            keywordQuery.TrimDuplicates = false;
            SearchExecutor searchExecutor = new SearchExecutor(clientContext);
            ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);
            clientContext.ExecuteQuery();
            return results;
        }

        public static ClientContext GetClientContext(ContentLogRequest request)
        {
            string locationUrl = $"{request.SiteUrl}";
            Uri locationUri = new Uri(locationUrl);
            ClientContext clientContext = new ClientContext(locationUri.Scheme + Uri.SchemeDelimiter + locationUri.Host);
            OfficeDevPnP.Core.AuthenticationManager authMgr = new OfficeDevPnP.Core.AuthenticationManager();
            clientContext = authMgr.GetSharePointOnlineAuthenticatedContextTenant(locationUrl, request.UserId, request.Password);
            clientContext.ExecuteQuery();
            return clientContext;

        }

        public static ClientContext GetWebContext(string webUrl, ContentLogRequest request,ClientContext ctx)
        {
            string locationUrl = $"{webUrl}";
            if (siteContextUrl != locationUrl)
            {
                siteContextUrl = locationUrl;
                ClientContext clientContext = new ClientContext(locationUrl.ToString());
                OfficeDevPnP.Core.AuthenticationManager authMgr = new OfficeDevPnP.Core.AuthenticationManager();
                clientContext = authMgr.GetSharePointOnlineAuthenticatedContextTenant(locationUrl.ToString(), request.UserId, request.Password);
                clientContext.ExecuteQuery();
                return clientContext;
            }
            return ctx;
        }
        private static IEnumerable<ComplianceTagModel> GetAllTagsForSite(ClientContext clientContext)
        {
            var web = clientContext.Web;
            clientContext.Load(web);
            clientContext.ExecuteQuery();

            var tags = SPPolicyStoreProxy.GetAvailableTagsForSite(clientContext, clientContext.Url);
            clientContext.ExecuteQuery();
            if (tags != null && tags.Count > 0)
            {
                return tags.Select(t => new ComplianceTagModel { TagId = t.TagId.ToString(), TagName = t.TagName });
            }
            return null;//  new PolicyTag() {TagId="",TagName="" }as IEnumerable<PolicyTag>;
        }

    }
}
