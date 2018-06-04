using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }
            Console.WriteLine(disco.Json);
            var tokenClient = new TokenClient(disco.TokenEndpoint, "clientId", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            Console.WriteLine(tokenResponse.Json);

            Console.WriteLine("------------------------------------------------------------");
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = client.GetAsync("http://localhost:5001/identity").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadKey();
        }
    }
}
