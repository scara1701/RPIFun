using RPIFun.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPIFun.Client.Services
{
    public class EnvService
    {
        Uri server;

        public EnvService(string serverAddress, string portNumber)
        {
            string serverstring = "http://" + serverAddress + ":" + portNumber;
            server = new Uri(serverstring);
        }

        public async Task<EnvResult> getEnvironment()
        {
            EnvResult envResult = new EnvResult();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = server;
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/environment/");

                if (response.IsSuccessStatusCode)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    envResult = JsonSerializer.Deserialize<EnvResult>(stringData, options);
                }
            }

            return envResult;
        }
    }
}
