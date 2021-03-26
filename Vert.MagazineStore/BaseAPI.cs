using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Vert.MagazineStore
{
    public class BaseAPI
    {
        #region  Variables
        private readonly string APIUrl = ConfigurationManager.AppSettings[Constants.MAGAZINESTOREAPIURL];
        #endregion

        /// <summary>
        /// Returns response from API
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAPIResponse(string apiMethod)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(APIUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MEDIATYPE));
                    HttpResponseMessage response = await httpClient.GetAsync($"{APIUrl}/{apiMethod}");
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post request to API
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAPIRequest(string apiMethod, string content)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                var httpContent = new StringContent(content, Encoding.UTF8, Constants.MEDIATYPE);
                using (var httpClient = new HttpClient())
                {
                    httpResponseMessage = await httpClient.PostAsync($"{APIUrl}/{apiMethod}", httpContent);
                    return httpResponseMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}