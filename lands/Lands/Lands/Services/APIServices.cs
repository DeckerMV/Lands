using Lands.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lands.Services
{
    public class APIServices
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Response() { IsSuccess = false, Message = "Can't connect, please turn on your internet connection." };

            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
                return new Response() { IsSuccess = false, Message = "Can't connect to internet, please verify your connection." };

            return new Response() { IsSuccess = true, Message = "Successfully connected" };
        }

        public async Task<TokenResponse> GetToken(string baseUrl, string username, string password)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage response = await client.PostAsync(
                    "Token",
                    new StringContent(string.Format("grant_type=password&username={0}$password={1}", username, password),
                                        Encoding.UTF8, "application/x-www-form-urlencoded")
                    );
                string resultJSON = await response.Content.ReadAsStringAsync();
                TokenResponse deserializedResult = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return deserializedResult;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Response> GetList<T>(string baseUrl, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                string url = string.Format("{0}{1}", servicePrefix, controller);
                HttpResponseMessage httpRequestResponse = await client.GetAsync(url);
                string resultJSON = await httpRequestResponse.Content.ReadAsStringAsync();

                if (!httpRequestResponse.IsSuccessStatusCode)
                    return new Response { IsSuccess = false, Message = "An error ocurred while trying to load the lands.", Result = resultJSON };

                List<T> list = JsonConvert.DeserializeObject<List<T>>(resultJSON);
                return new Response { IsSuccess = true, Message = "List successfully created", Result = list };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = ex.Message };
            }
        }
    
    }
}
