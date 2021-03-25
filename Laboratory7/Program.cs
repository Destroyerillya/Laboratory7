using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Laboratory7
{
    class Program
    {
        public class Address
        {
            public string road { get; set; }
            public string village { get; set; }
            public string state_district { get; set; }
            public string state { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
        }

        public class Root
        {
            public string lat { get; set; }
            public string lon { get; set; }
            public string addresstype { get; set; }
            public string display_name { get; set; }
            public Address address { get; set; }
        }

        static async Task Main(string[] args)
        {
            bool running = true;
            while (running)
            {   
                Console.WriteLine("Enter 1 to use api or 2 to exit");
                string caseSwitch = Console.ReadLine();

                switch (caseSwitch)
                {
                    case "1":
                        Console.WriteLine("Enter lat");
                        string lat = Console.ReadLine();
                        Console.WriteLine("Enter lon");
                        string lon = Console.ReadLine();
                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                            HttpResponseMessage pointsResponse =
                                await client.GetAsync("https://nominatim.openstreetmap.org/reverse?lat=" + lat + "&lon=" + lon +
                                                      "&format=jsonv2");

                            if (pointsResponse.IsSuccessStatusCode)
                            {
                                Root roots = await pointsResponse.Content.ReadFromJsonAsync<Root>();
                                try
                                {
                                    Console.WriteLine("Country:" + roots.address.country);
                                    Console.WriteLine("Country_code:" + roots.address.country_code);
                                    Console.WriteLine("Postcode:" + roots.address.postcode);
                                    Console.WriteLine("Road:" + roots.address.road);
                                    Console.WriteLine("Village:" + roots.address.village);
                                    Console.WriteLine("State_district:" + roots.address.state_district);
                                    Console.WriteLine("State:" + roots.address.state);
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Sea");
                                }
                            }
                            else
                            {
                                string resp = await pointsResponse.Content.ReadAsStringAsync();
                                Console.WriteLine(resp);
                            }
                        }
                        break;
                    case "2":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
        }
    }
}