using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Common
{
    public class HttpRequestHelper
    {
        public static string SendPostNoToken(string urlPath, string baseUrl, string jsonBody)
        {
            try
            {
                var options = new RestClientOptions(urlPath)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseUrl, Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddStringBody(jsonBody, DataFormat.Json);
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static string SendGetNoToken(string urlPath, string baseUrl)
        {
            try
            {
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(urlPath);
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    //GET Method
                //    HttpResponseMessage response =  client.GetAsync(baseUrl);
                //    if (response.IsSuccessStatusCode)
                //    {
                //        var aa = await response.Content.ReadAsStringAsync();
                //        return aa;
                //    }
                //    else
                //    {
                //        Console.WriteLine("Internal server Error");
                //    }

                return string.Empty;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string SendPostToken(string urlPath, string baseUrl, string jsonBody, string token)
        {
            try
            {
                var options = new RestClientOptions(urlPath)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseUrl, Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddStringBody(jsonBody, DataFormat.Json);
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static async Task<string> SendHttpClient(string baseUrl, string api_url, string jsonData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(api_url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();


                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
