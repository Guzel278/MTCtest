using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MTCtest
{
    internal class Request
    {
        public async Task<string> PostRequest(string id, string name)
        {
            var _clientHandler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                UseCookies = true
            };

            var HttpClientTest = new HttpClient(_clientHandler) { BaseAddress = new Uri("https://httpbin.org/") };

            RequestModel model = new RequestModel
            {
                Id = id,
                Name = name
            };

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postRequest = new HttpRequestMessage
            {
              //  Headers = new HttpResponseMessage(),
                RequestUri = new Uri("https://httpbin.org/anything"),
                Method = HttpMethod.Post,
                Content = content
               
            };
            var postResponse = HttpClientTest.Send(postRequest);
            postResponse.EnsureSuccessStatusCode();
            var jsonPost = await postResponse.Content.ReadAsStringAsync();            
            var sResponse =  JsonConvert.DeserializeObject(jsonPost);
            string response = string.Concat(sResponse, postResponse);
            return JsonConvert.SerializeObject(response);            
        }
    }
}
