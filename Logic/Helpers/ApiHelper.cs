using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Projekt_WPF_TODO_app.Logic.Helpers
{
    public class ApiHelper
    {
        private HttpClient client;
        public ApiHelper(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }

        public string SendPostRequest(string json, string endPoint)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(endPoint, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            else
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseContent) ?? throw new ArgumentException();
                List<string> values = data.Values.FirstOrDefault() ?? throw new ArgumentException();
                string formattedText = String.Join(" ", values);
                return formattedText;
            }
        }
        public string SendGetRequest(string endPoint)
        {
            var response = client.GetAsync(endPoint).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            else
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseContent) ?? throw new ArgumentException();
                List<string> values = data.Values.FirstOrDefault() ?? throw new ArgumentException();
                string formattedText = String.Join(" ", values);
                return formattedText;
            }
        }

        public string SendPostRequestWithHeaders(string json, string endPoint, string authToken)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", authToken);
            var response = client.PostAsync(endPoint, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            else
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseContent) ?? throw new ArgumentException();
                List<string> values = data.Values.FirstOrDefault() ?? throw new ArgumentException();
                string formattedText = String.Join(" ", values);
                return formattedText;
            }
        }

        public string SendDeleteRequest(string endPoint, string authToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", authToken);
            var response = client.DeleteAsync(endPoint).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            else
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseContent) ?? throw new ArgumentException();
                List<string> values = data.Values.FirstOrDefault() ?? throw new ArgumentException();
                string formattedText = String.Join(" ", values);
                return formattedText;
            }
        }




    }
}
