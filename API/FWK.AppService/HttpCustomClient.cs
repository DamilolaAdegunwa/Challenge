using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain;

namespace FWK.AppService
{
    /// <summary>
    /// This class handles all the Network call.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpCustomClient<T>
    {

        private string baseUrl;

        private string access_token;
        public HttpCustomClient(string _baseUrl, string _access_token)
        {
            baseUrl = _baseUrl;

            access_token = _access_token;
        }


        /// <summary>
        /// Method for GET request.
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<T> GetRequest(string api)
        {
            return await this.GetRequest<T>(api);
        }



        /// <summary>
        /// Method for GET request.
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<RT> GetRequest<RT>(string api)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.baseUrl);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(access_token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);
                    }


                    // HTTP GET
                    HttpResponseMessage response = await client.GetAsync(api);

                    this.ManageErrorsClient(response);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var responseObject = JsonConvert.DeserializeObject<RT>(responseString);
                        return responseObject;

                    }
                    return default(RT);
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() => App.LogError(ex));
                throw ex;
            }
        }

        /// <summary>
        /// Method for the POST request.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T> PostRequest(string api, T data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(access_token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);
                    }

                    var jsonRequest = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                    // HTTP POST
                    var response = await client.PostAsync(api, content);

                    this.ManageErrorsClient(response);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var responseObject = JsonConvert.DeserializeObject<T>(responseString);
                        return responseObject;

                    }
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() => App.LogError(ex));
                throw ex;
            }
        }

        /// <summary>
        ///  Method for the POST request.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="jsonInput"></param>
        /// <returns></returns>
        public async Task<RT> PostRequest<RT>(string api, string jsonInput = "{}")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(access_token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);
                    }

                    var content = new StringContent(jsonInput, Encoding.UTF8, "text/json");
                    var response = await client.PostAsync(api, content);

                    this.ManageErrorsClient(response);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var responseObject = JsonConvert.DeserializeObject<RT>(responseString);
                        return responseObject;
                    }
                    return default(RT);
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() => App.LogError(ex));
                throw ex;
            }
        }

        public virtual async Task<T> PostRequest(string api, List<KeyValuePair<string, string>> FormData)
        {
            try
            {
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(this.baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new FormUrlEncodedContent(FormData.ToArray());

                    // HTTP POST
                    HttpResponseMessage response = await client.PostAsync(api, content);

                    this.ManageErrorsClient(response);

                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        T responseObject = JsonConvert.DeserializeObject<T>(responseString);
                        return responseObject;
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() => App.LogError(ex));
                throw ex;
            }
        }




        /// <summary>
        /// Method for the PUT request.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="jsonInput"></param>
        /// <returns></returns>
        public async Task<string> PutRequest(string api, string jsonInput)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(access_token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);
                    }

                    var content = new StringContent(jsonInput, Encoding.UTF8, "text/json");
                    var res = await client.PutAsync(api, content);

                    this.ManageErrorsClient(res);

                    if (res.IsSuccessStatusCode)
                    {
                        var responseString = res.Content.ReadAsStringAsync().Result;
                        return responseString;

                    }

                    return "";
                }
            }
            catch (Exception ex)
            {
                //await Task.Run(() => App.LogError(ex));
                throw ex;
            }
        }

        /// <summary>
        /// Method for Deserialize the JSON response.
        /// </summary>
        /// <param name="responseString"></param>
        /// <returns></returns>
        public T DeserializeObject(string responseString)
        {
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        //protected virtual void ManageErrorsClient(AbstractModel response)
        //{
        //    //TODO: manage the response
        //    if (response != null && response.Status != ActionStatus.Ok.ToString())
        //    {
        //        throw new Exception(String.Join(",", response.Messages));
        //    }
        //}


        protected virtual void ManageErrorsClient(HttpResponseMessage response)
        {
            //TODO: manage the response
            if (response != null && response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                //if (responseString.Contains(SharedTokens.RegistrarUsuario))
                //{
                //    throw new Exceptions.AuthenticationException(SharedTokens.RegistrarUsuario);
                //}
                //if (responseString.Contains(SharedTokens.RequierdToken))
                //{
                //    throw new Exceptions.RequierdTokenException(SharedTokens.RequierdToken);
                //}
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new System.UnauthorizedAccessException(responseString);
                }

                

                throw new Exception(responseString);


            }
        }
    }
}
