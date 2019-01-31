using Colligo.O365Product.Helper;
using Colligo.O365Product.Helper.Constant;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.Model;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Colligo.O365Product.SharePointData
{
    public class SPBrowse
    {
        // Create SharePoint site context        
        public static string GetAllContentTypesBySiteUrl(SharepointRequest request)
        {
            try
            {
                using (ClientContext context = GetClientContext(request))
                {
                    ContentTypeCollection contentTypeColl = context.Web.ContentTypes;
                    context.Load(contentTypeColl);
                    context.ExecuteQuery();
                    List<SPContentType> contentTypes = new List<SPContentType>();
                    foreach (ContentType contentType in contentTypeColl)
                    {
                        string id = contentType.Id.ToString();
                        if (contentType.Group != "_Hidden" && !contentType.Hidden && (id.StartsWith(SPDocumentContentType.DocumentContentType) || id.StartsWith(SPDocumentContentType.DocumentSetContentType)))
                            contentTypes.Add(new SPContentType()
                            {
                                Id = id,
                                Name = contentType.Name,
                                Selected = false
                            });
                    }
                    return JsonHelper.ConvertToJson(contentTypes);
                }
            }
            catch
            {
                throw;
            }
        }

        // Get fields of the content type
        public static string GetAllFieldsByContentId(SharepointRequest request)
        {
            string configJson = string.Empty;
            var ctype = request.Type;
            string ServerRelativeUrl = Utility.GetRelativeUrl(request.ServerRelativeUrl);
            var title = request.Name;

            ClientContext clientContext = GetClientContext(request);
            using (clientContext)
            {
                ContentType contentType = clientContext.Web.ContentTypes.GetById(request.ContentTypeId);
                clientContext.Load(contentType);
                clientContext.ExecuteQuery();
                var fields = contentType.Fields;
                clientContext.Load(fields, types => types.Include(type => type.Id, type => type.Title,
                type => type.TypeDisplayName, type => type.DefaultValue, type => type.TypeAsString,
                type => type.InternalName, type => type.Required, type => type.ReadOnlyField,
                type => type.StaticName, type => type.Hidden));
                clientContext.Load(fields);
                clientContext.ExecuteQuery();
                List<SharePointField> lFields = new List<SharePointField>();
                foreach (var field in fields)
                {

                    if (!field.FromBaseType && field.TypeAsString != FieldTypesConst.FieldLookupMulti && field.InternalName != FieldTypesConst.FieldTaxKeywordHT && field.Group != "_Hidden" && !field.Hidden)
                    {
                        if ((field.TypeAsString.ToLower() == FieldTypesConst.FieldDate.ToLower()) || (field.TypeAsString.ToLower() == FieldTypesConst.FieldDateOnly.ToLower()))
                        {
                            lFields.Add(GetSpFieldProperties(field, clientContext));
                        }
                    }
                }
                return JsonHelper.ConvertToJson(lFields);
            }
        }

        public static SharePointField GetSpFieldProperties(Field field, ClientContext clientContext)
        {
            try
            {
                SharePointField sfields = new SharePointField();
                sfields.FieldId = !string.IsNullOrEmpty(Convert.ToString(field.Id)) ? field.Id.ToString() : "";
                sfields.Title = !string.IsNullOrEmpty(Convert.ToString(field.Title)) ? field.Title.ToString() : "";
                sfields.DisplayName = !string.IsNullOrEmpty(Convert.ToString(field.Title)) ? field.Title.ToString() : "";
                sfields.TypeAsStr = !string.IsNullOrEmpty(Convert.ToString(field.TypeAsString)) ? field.TypeAsString.ToString() : "";
                sfields.InternalName = !string.IsNullOrEmpty(Convert.ToString(field.InternalName)) ? field.InternalName.ToString() : "";
                sfields.Required = !string.IsNullOrEmpty(Convert.ToString(field.Required)) ? field.Required.ToString() : "";
                return sfields;
            }
            catch
            {
                throw;
            }
        }

        public static string SearchSharepointSite(SharepointRequest request)
        {
             string token = request.AccessToken;
            string relativeUrl = Utility.GetServerRelativeUrl(request.Url, request.ServerRelativeUrl);
            ClientContext clientContext = GetClientContext(request);
            using (clientContext)
            {
                KeywordQuery keywordQuery = new KeywordQuery(clientContext);
                keywordQuery.ClientType = "ContentSearchRegular";
                keywordQuery.StartRow = 0;
                keywordQuery.QueryText = "(Site:" + request.Url + ") AND ((contentclass=STS_Site) AND ((WebTemplate <>POLICYCTR) AND (WebTemplate <>POINTPUBLISHINGHUB) AND (WebTemplate <>POINTPUBLISHINGTOPIC) AND (-SPSiteUrl:personal)))";
                StringCollection selectProperties = keywordQuery.SelectProperties;
                keywordQuery.TrimDuplicates = true;
                keywordQuery.StartRow = 0;
                keywordQuery.RowLimit = 50;
                keywordQuery.SelectProperties.Add("Title");
                keywordQuery.SelectProperties.Add("Path");
                keywordQuery.SelectProperties.Add("SiteName");
                keywordQuery.SelectProperties.Add("SiteTitle");
                keywordQuery.SelectProperties.Add("ContentTypeId");
                keywordQuery.SelectProperties.Add("SPSiteUrl");
                keywordQuery.SelectProperties.Add("SPWebUrl");
                keywordQuery.SelectProperties.Add("Type");
                keywordQuery.TrimDuplicates = false;
                SearchExecutor searchExecutor = new SearchExecutor(clientContext);
                clientContext.ExecutingWebRequest += new EventHandler<WebRequestEventArgs>((s, e) => context_ExecutingWebRequest(s, e, token));
                ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);
                clientContext.ExecuteQuery();
                List<SharepointRequest> spRequests = new List<SharepointRequest>();
                foreach (var resultRow in results.Value[0].ResultRows)
                {
                    SharepointRequest spRequest = new SharepointRequest();
                    string url = !string.IsNullOrEmpty(Convert.ToString(resultRow["SPSiteUrl"])) ? resultRow["SPSiteUrl"].ToString() : "";
                    spRequest.Url = url;
                    spRequest.Name = !string.IsNullOrEmpty(Convert.ToString(resultRow["Title"])) ? resultRow["Title"].ToString() : "";
                    if (!string.IsNullOrEmpty(Convert.ToString(resultRow["Path"])))
                    {
                        Uri uri = new Uri(resultRow["Path"].ToString());
                        string serverRelativeURL = uri.AbsolutePath;
                        spRequest.ServerRelativeUrl = serverRelativeURL;
                    }
                    spRequests.Add(spRequest);
                }
                return JsonHelper.ConvertToJson(spRequests);
            }
        }

        public static ClientContext GetClientContext(SharepointRequest request)
        {
            if (!request.ServerRelativeUrl.StartsWith("/"))
                request.ServerRelativeUrl = "/" + request.ServerRelativeUrl;
            string locationUrl = $"{request.Url}{Utility.GetRelativeUrl(request.ServerRelativeUrl)}";
            Uri locationUri = new Uri(locationUrl);
            var t = request.AccessToken;
            ClientContext RootContext = new ClientContext(locationUri.Scheme + Uri.SchemeDelimiter + locationUri.Host);
            RootContext.ExecutingWebRequest += new EventHandler<WebRequestEventArgs>((s, e) => context_ExecutingWebRequest(s, e, t));
            Web web = RootContext.Web;
            RootContext.Load(web);
            RootContext.ExecuteQuery();
            Uri webUrl = Web.WebUrlFromFolderUrlDirect(RootContext, locationUri);
            ClientContext clientContext = new ClientContext(webUrl.ToString());
            clientContext.ExecutingWebRequest += new EventHandler<WebRequestEventArgs>((s, e) => context_ExecutingWebRequest(s, e, t));
            return clientContext;
        }

        public static List GetLibraryByContext(SharepointRequest request, ClientContext context)
        {
            string Url = Utility.GetServerRelativeUrl(request.Url, request.ServerRelativeUrl).Remove(0, 1);

            List library = context.Web.GetList(Url);
            context.Load(library);
            context.ExecuteQuery();
            string title = library.Title;
            return library;
        }

        static void context_ExecutingWebRequest(object sender, WebRequestEventArgs e, string token)
        {
            e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + token;

        }

    }
}
