using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vert.MagazineStore
{
    public class MagazineAPI : BaseAPI
    {
        /// <summary>
        /// Returns token from API
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetToken()
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                HttpResponseMessage responseMessage = await GetAPIResponse(Constants.TOKENAPI);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    aPIResponse = JsonConvert.DeserializeObject<APIResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return aPIResponse;
        }

        /// <summary>
        /// Returns subscribers with the magazine Id's to which they are subscribed
        /// </summary>
        /// <returns></returns>
        public async Task<CategoryResponse> GetCategories()
        {
            CategoryResponse categories = new CategoryResponse();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await GetAPIResponse($"{Constants.CATEGORYAPI}/{token}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    categories = JsonConvert.DeserializeObject<CategoryResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categories;
        }

        /// <summary>
        /// Returns subscribers with the magazine Id's to which they are subscribed
        /// </summary>
        /// <returns></returns>
        public async Task<SubscriberResponse> GetSubscribers()
        {
            SubscriberResponse subscribers = new SubscriberResponse();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await GetAPIResponse($"{Constants.SUBSCRIBERAPI}/{token}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    subscribers = JsonConvert.DeserializeObject<SubscriberResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return subscribers;
        }

        /// <summary>
        /// Returns a list of magazines for a given category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<MagazineResponse> GetMagazineByCategory(string category)
        {
            MagazineResponse magazines = new MagazineResponse();
            try
            {
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await GetAPIResponse($"{Constants.MAGAZINEAPI}/{token}/{category}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = readTask.GetAwaiter().GetResult();
                    magazines = JsonConvert.DeserializeObject<MagazineResponse>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return magazines;
        }

        /// <summary>
        /// Post answer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> PostAnswer(AnswerRequest request)
        {
            string result = string.Empty;
            try
            {
                string content = JsonConvert.SerializeObject(request);
                var token = GetToken().Result.Token;
                HttpResponseMessage responseMessage = await PostAPIRequest($"{Constants.ANSWERAPI}/{token}", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = readTask.GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}