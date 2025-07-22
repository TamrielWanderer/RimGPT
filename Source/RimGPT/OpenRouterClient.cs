using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RimGPT
{
    public class OpenRouterClient : IAIClient
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string? _apiKey;

        public OpenRouterClient(string? apiKey = null)
        {
            _apiKey = apiKey ?? Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
        }

        public async Task<string?> GenerateAsync(string prompt)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("OpenRouter API key not set. Set OPENROUTER_API_KEY environment variable or pass in an apiKey.");
            }

            var requestBody = new
            {
                model = "openrouter/gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var response = await _client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(json);
            return data?.choices[0]?.message?.content;
        }
    }
}
