using Colligo.O365Product.HttpUtility;
using Colligo.O365Product.HttpUtility.Generic;
using Colligo.O365Product.LoggerService;
using Colligo.O365Product.ServiceInterface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.HttpUtility.ServiceHelper
{
    public class HttpClientHelper
    {
        protected readonly ILogManager _logger;
        public HttpClientHelper(ILogManager logger)
        {
            _logger = logger;
        }
        protected string ServiceUrl { get; set; }

        protected string Servicetoken { get; set; }
        #region API caller

        private void SetToken<T>(ApiHttpClient<T> httpClient) where T : class
        {
            if (Servicetoken != null)
                httpClient.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Servicetoken);
        }

        private void SetToken<S, R>(ApiHttpClient<S, R> httpClient) where S : class where R : class
        {
            if (Servicetoken != null)
                httpClient.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Servicetoken);
        }


        protected T GetFromAPI<T>(string apiMethodwithParameter, out bool error) where T : class
        {
            error = false;
            WebApiResult<T> result = null;
            try
            {
                ApiHttpClient<T> httpClient = new ApiHttpClient<T>(ServiceUrl, false);
                SetToken(httpClient);
                result = httpClient.GetAsync(apiMethodwithParameter).Result;

                if (!result.HasError)
                {
                    return result.Data;
                }
                else
                {
                    error = true;
                    Error(apiMethodwithParameter, result.ErrorMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            return null;
        }


        /// <summary>
        /// Generic method for getting API client request for single source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiMethodwithParameter"></param>
        /// <returns></returns>
        protected List<T> GetManyFromAPI<T>(string apiMethodwithParameter) where T : class
        {
            WebApiResult<IEnumerable<T>> result = null;
            try
            {
                ApiHttpClient<T> httpClient = new ApiHttpClient<T>(ServiceUrl, false);
                SetToken(httpClient);
                result = httpClient.GetManyAsync(apiMethodwithParameter).Result;

                if (!result.HasError)
                {
                    if (result.Data != null)
                        return result.Data.ToList();
                    else
                        return null;
                }
                else
                {
                    Error(apiMethodwithParameter, result.ErrorMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

            }
            return null;
        }

        /// <summary>
        /// Generic method for getting many with post
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="searchCriteria"></param>
        /// <param name="apiMethod"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected List<R> GetManyByPostFromAPI<S, R>(S searchCriteria, string apiMethod, out bool error, double timeOutinMinute = 30, bool setTimeout = false) where S : class where R : class
        {
            error = false;
            try
            {
                error = false;
                ApiHttpClient<S, R> httpClient = new ApiHttpClient<S, R>(ServiceUrl, false);
                SetToken(httpClient);
                if (setTimeout)
                    httpClient.Client.Timeout = TimeSpan.FromMinutes(timeOutinMinute);
                WebApiResult<IEnumerable<R>> result = httpClient.PostAndGetManyAsync(apiMethod, searchCriteria).Result;
                if (!result.HasError)
                {
                    if (result.Data != null)
                        return result.Data.ToList();
                    else
                        return null;
                }
                else
                {
                    error = true;
                    Error(apiMethod, result.ErrorMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

            }
            return null;
        }

        /// <summary>
        /// Add method for posting data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiMethod"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected T PostToApi<T>(string apiMethod, T data, out bool error) where T : class
        {
            error = false;
            try
            {
                ApiHttpClient<T> httpClient = new ApiHttpClient<T>(ServiceUrl, false);
                SetToken(httpClient);
                WebApiResult<T> postResult = httpClient.PostAsync(apiMethod, data).Result;
                if (!postResult.HasError)
                {
                    return postResult.Data;
                }
                else
                {
                    error = true;
                    Error(apiMethod, postResult.ErrorMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

            }
            return null;
        }

        /// <summary>
        /// Method for posting data when expecting a returned data also
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="apiMethod"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected S PostToApi<T, S>(string apiMethod, T data, out bool error) where T : class where S : class
        {
            error = false;
            try
            {
                ApiHttpClient<T, S> httpClient = new ApiHttpClient<T, S>(ServiceUrl, false);
                SetToken(httpClient);
                WebApiResult<S> postResult = httpClient.PostAsync(apiMethod, data).Result;
                if (!postResult.HasError)
                {
                    return postResult.Data;
                }
                else
                {
                    error = true;
                    Error(apiMethod, postResult.ErrorMessages);
                    return postResult.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            return null;
        }

        public void Error(string message, List<string> exception)
        {
            if (exception != null && exception.Count > 0)
            {
                if (message.IndexOf("colligoaddin/token") == -1)
                {
                    if (exception.Any(p => p.StartsWith("Response status code does not indicate success: 401")))
                        Servicetoken = null;
                }
            }
            _logger.Error(message + string.Join(",", exception));
        }
        #endregion
    }
}
