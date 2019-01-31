using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.SharePointHelper
{
    public static class SharePointHelper
    {
        /// <summary>
        /// Retrieve formatted Term string
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        /// 
        public const int DefaultLanguage = 1033;

        public const string TaxonomyGuidLabelDelimiter = "|";
     
        public static void context_ExecutingWebRequest(object sender, WebRequestEventArgs e, string token)
        {
            e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + token;

        }

        public static Term EnsureKeyword(TaxonomyField taxField, string name)
        {
            var ctx = taxField.Context;
            var taxSession = TaxonomySession.GetTaxonomySession(ctx);
            var termStore = taxSession.GetDefaultKeywordsTermStore();
            var keywords = termStore.KeywordsTermSet.GetAllTerms();
            var result = ctx.LoadQuery(keywords.Where(k => k.Name == name));
            ctx.ExecuteQuery();
            var keyword = result.FirstOrDefault();
            if (keyword != null)
            {
                return keyword;
            }
            keyword = termStore.KeywordsTermSet.CreateTerm(name, DefaultLanguage, Guid.NewGuid());
            ctx.Load(keyword);
            ctx.ExecuteQuery();
            return keyword;
        }

        public static string GetTermString(Term term)
        {
            return string.Format("-1;#{0}{1}{2}", term.Name, TaxonomyGuidLabelDelimiter, term.Id);
        }

        public static string GetHostUrl(String webUrl)
        {
            Uri uri = new Uri(webUrl);
            string hostUrl = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
            return hostUrl;
        }

        public static string GetTermsString(IEnumerable<Term> terms)
        {
            var termsString = terms.Select(GetTermString).ToList();
            return string.Join(";#", termsString);
        }

        public static string GetTermIdForTerm(string term,
       Guid termSetId, ClientContext clientContext)
        {
            string termId = string.Empty;

            TaxonomySession tSession = TaxonomySession.GetTaxonomySession(clientContext);
            TermStore ts = tSession.GetDefaultSiteCollectionTermStore();
            TermSet tset = ts.GetTermSet(termSetId);

            LabelMatchInformation lmi = new LabelMatchInformation(clientContext);

            lmi.Lcid = 1033;
            lmi.TrimUnavailable = true;
            lmi.TermLabel = term;

            TermCollection termMatches = tset.GetTerms(lmi);
            clientContext.Load(tSession);
            clientContext.Load(ts);
            clientContext.Load(tset);
            clientContext.Load(termMatches);

            clientContext.ExecuteQuery();

            if (termMatches != null && termMatches.Count() > 0)
                termId = termMatches.First().Id.ToString();

            return termId;

        }
    }
}
