using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace RPIFun.Client.Services
{
    public class LEDService
    {
        Uri server;
        public LEDService(string serverAddress, string portNumber)
        {
            string serverstring = "http://" + serverAddress + ":" + portNumber;
            server = new Uri(serverstring);
        }

        public async Task<bool> SwitchLEDStatus()
        {
            return await GetLEDAction("/api/LED/switch");
        }
        public async Task<bool> GetLEDStatus()
        {
            return await GetLEDAction("/api/LED");
        }

        public async Task<bool> GetLEDAction(string action)
        {
            bool LED = false;

            using(HttpClient client = new HttpClient()) {
                client.BaseAddress = server;
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(action);

                if (response.IsSuccessStatusCode)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    LED = JsonSerializer.Deserialize<bool>(stringData, options);
                }
            }
            return LED;
        }
    }
}
