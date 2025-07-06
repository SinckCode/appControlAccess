using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using appControlAccess1.Models;

namespace appControlAccess1.Services
{
    public static class AccessLogService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new System.Uri(ApiConfig.BaseUrl)
        };

        public static async Task<List<AccessLog>> GetRecentLogsAsync()
        {
            var response = await _httpClient.GetAsync("/api/access/logs");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<AccessLogResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root?.Logs ?? new List<AccessLog>();
        }
    }

    public class AccessLogResponse
    {
        public bool Success { get; set; }
        public List<AccessLog> Logs { get; set; }
    }
}
