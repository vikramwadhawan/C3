using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Colligo.O365Product.HttpUtility.Generic
{
    public class ApiHttpClient<Stype, Rtype> : IDisposable
        where Stype : class
        where Rtype : class
    {
        private bool disposed = false;
        private Uri apiUrl;
        HttpClient httpClient = new HttpClient();

        public ApiHttpClient(string serviceBaseUrl, bool setToken = true)
        {
            this.apiUrl = new Uri(serviceBaseUrl);
            Initialize(setToken);
        }
        public HttpClient Client
        {
            get { return httpClient; }
        }

        public void Initialize(bool setToken)
        {
            httpClient.BaseAddress = apiUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            if (setToken)
                ConfigAuthToken();
        }

        private void ConfigAuthToken()
        {
            AuthenticationToken authToken = null;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["Id_token"] != null)
                        authToken = HttpContext.Current.Session["Id_token"] as AuthenticationToken;
                }
            }
            if (authToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authToken.token_type, authToken.access_token);
        }

        public void SetAuthToken(AuthenticationToken authToken)
        {
            if (authToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authToken.token_type, authToken.access_token);
        }

        public async Task<WebApiResult<IEnumerable<Rtype>>> PostAndGetManyAsync(string apiPostMethod, Stype postObject)
        {
            HttpResponseMessage response;
            WebApiResult<IEnumerable<Rtype>> result = new WebApiResult<IEnumerable<Rtype>>();
            result.ErrorMessages = new List<string>();
            try
            {
                response = await httpClient.PostAsJsonAsync(apiPostMethod, postObject).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    result.ErrorMessages.Add(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                    result.HasError = true;
                }
                await response.Content.ReadAsAsync<IEnumerable<Rtype>>().ContinueWith(x =>
                {
                    if (!x.IsFaulted)
                    {

                        result.Data = x.Result;
                    }
                    else
                    {
                        result.ErrorMessages.Add(x.Exception.InnerException.Message);
                        result.HasError = true;
                    }
                });
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add(ex.Message);
                result.HasError = true;
            }
            return result;
        }

        public async Task<WebApiResult<Rtype>> PostAsync(string apiPostMethod, Stype postObject)
        {
            Rtype resultObject = null;
            HttpResponseMessage response;
            WebApiResult<Rtype> result = new WebApiResult<Rtype>();
            result.ErrorMessages = new List<string>();
            try
            {
                bool isHttpcontent = postObject is HttpContent;
                if (isHttpcontent)
                {
                    response = await httpClient.PostAsync(apiPostMethod, postObject as HttpContent).ConfigureAwait(false);
                }
                else
                    response = await httpClient.PostAsJsonAsync(apiPostMethod, postObject).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    result.ErrorMessages.Add(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                    result.HasError = true;
                }
                await response.Content.ReadAsAsync<Rtype>().ContinueWith(x =>
                {
                    if (!x.IsFaulted)
                    {
                        resultObject = x.Result;
                        result.Data = resultObject;
                    }
                    else
                    {
                        result.ErrorMessages.Add(x.Exception.InnerException.Message);
                        result.HasError = true;
                    }
                });
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add(ex.Message);
                result.HasError = true;
            }
            return result;
        }

        public async Task<WebApiResult<Rtype>> PutAsync(string apiUpdateMethod, Stype putObject)
        {
            Rtype resultObject = null;
            HttpResponseMessage response;
            WebApiResult<Rtype> result = new WebApiResult<Rtype>();
            result.ErrorMessages = new List<string>();
            try
            {
                response = await httpClient.PutAsJsonAsync(apiUpdateMethod, putObject).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsAsync<Rtype>().ContinueWith(x =>
                    {
                        if (!x.IsFaulted)
                        {
                            resultObject = x.Result;
                            result.Data = resultObject;
                        }
                        else
                        {
                            result.ErrorMessages.Add(x.Exception.InnerException.Message);
                            result.HasError = true;
                        }
                    });
                }
                else
                {
                    result.ErrorMessages.Add(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                    result.HasError = true;
                }
            }
            catch (Newtonsoft.Json.JsonException jsonEx)
            {
                result.ErrorMessages.Add(jsonEx.Message);
                result.HasError = true;
            }
            catch (HttpRequestException httpEx)
            {
                result.ErrorMessages.Add(httpEx.Message);
                result.HasError = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add(ex.Message);
                result.HasError = true;
            }
            return result;
        }

        public async Task<WebApiResult<Rtype>> DeleteRequest(string apiDeleteMethod)
        {
            Rtype resultObject = null;
            HttpResponseMessage response;
            WebApiResult<Rtype> result = new WebApiResult<Rtype>();
            result.ErrorMessages = new List<string>();
            try
            {
                response = await httpClient.DeleteAsync(apiDeleteMethod).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsAsync<Rtype>().ContinueWith(x =>
                    {
                        if (!x.IsFaulted)
                        {
                            resultObject = x.Result;
                            result.Data = resultObject;
                        }
                        else
                        {
                            result.ErrorMessages.Add(x.Exception.InnerException.Message);
                            result.HasError = true;
                        }
                    });
                }
                else
                {
                    result.ErrorMessages.Add(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
                    result.HasError = true;
                }
            }
            catch (Newtonsoft.Json.JsonException jsonEx)
            {
                result.ErrorMessages.Add(jsonEx.Message);
                result.HasError = true;
            }
            catch (HttpRequestException httpEx)
            {
                result.ErrorMessages.Add(httpEx.Message);
                result.HasError = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add(ex.Message);
                result.HasError = true;
            }
            return result;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                {
                    var hc = httpClient;
                    httpClient = null;
                    hc.Dispose();
                }
                disposed = true;
            }
        }

        #endregion IDisposable Members
    }
}
