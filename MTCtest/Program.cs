using System;
using Newtonsoft.Json;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MTCtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input string id:");
            string id = Console.ReadLine();
            Console.WriteLine("input string name:");
            string name = Console.ReadLine();
            Console.WriteLine("input path:");
            string path = Console.ReadLine();          
            var response = PostRequest(id, name);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, response);
            }
        }
        static async Task<string> PostRequest(string id, string name)
        {
            var client = new HttpClient();

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://httpbin.org/anything"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = name
                }), Encoding.UTF8, "application/json")
            };
            var postResponse = client.Send(postRequest);
            var result = new { ResultCode = postResponse.StatusCode, Content = await postResponse.Content.ReadAsStringAsync() };
            return JsonConvert.SerializeObject(result);
        }
    }
}
