using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RimGPT
{
    public class GeminiClient : IAIClient
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string? _apiKey;

        public GeminiClient(string? apiKey = null)
        {
            _apiKey = apiKey ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        }

        public async Task<string?> GenerateAsync(string prompt)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("Gemini API key not set. Set GEMINI_API_KEY environment variable or pass in an apiKey.");
            }

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";
            var response = await _client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(json);
            return data?.candidates[0]?.content?.parts[0]?.text;
        }
    }
}
