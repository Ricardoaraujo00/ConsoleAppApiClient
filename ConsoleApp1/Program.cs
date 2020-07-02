using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ConsoleApp1
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        static string product = null;

        static string company = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Console.WriteLine(configuration.GetSection("Company").Value);

            //Dictionary<string, string> kioskconfig = new Dictionary<string, string>();
            //configuration.GetSection("kioske").GetChildren().ToList().ForEach(x => kioskconfig[x.Key] = x.Value);
            //foreach (var item in kioskconfig)
            //{
            //    Console.WriteLine($"key: {item.Key}, Value: {item.Value}");
            //}

            //GetDataStringArray();
            GetDataDictionary();

        }

        public static void GetDataDictionary()
        {
            Console.WriteLine("get dictionary");
            //Consuming the API
            //Specify web API base address
            client.BaseAddress = new Uri("http://localhost:53309/");

            //Specify headers
            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            //Make the calls
            try
            {
                var action = "api/kioskconfig";
                var request = client.GetAsync(action);
                var response =
                    request.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine("have result");
                Dictionary<string,string> msgs = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                foreach (var item in msgs)
                {
                    Console.WriteLine($"key:{item.Key}, value:{item.Value}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            Console.ReadKey();
        }


        public static void GetDataStringArray()
        {
            //Consuming the API
            //Specify web API base address
            client.BaseAddress = new Uri("http://localhost:53309/");

            //Specify headers
            var val = "application/json";
            var media = new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            //Make the calls
            try
            {
                var action = "api/mensagens";
                var request = client.GetAsync(action);
                var response =
                    request.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(response);
                string[] msgs = JsonConvert.DeserializeObject<string[]>(response);
                string msgsconcat = "";
                int i = 0;
                int compr = msgs.Length;
                foreach (var item in msgs)
                {
                    i++;
                    if (i == 1)
                    {
                        company = item;
                        Console.WriteLine(company);
                    }
                    else
                    {
                        Console.WriteLine(item);
                        msgsconcat += item;
                        if (i < compr)
                        {
                            msgsconcat += "|";
                        }

                    }



                }
                Console.WriteLine(msgsconcat);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}
