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
            Request request = new Request();
            var response = request.PostRequest(id, name);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, response);
            }
        }      
    }
 }
