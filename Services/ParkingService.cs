using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using appControlAccess1.Models;

namespace appControlAccess1.Services
{
    public static class ParkingService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new System.Uri(ApiConfig.BaseUrl)
        };

        public static async Task<List<ParkingSpot>> GetParkingStatusAsync()
        {
            var response = await _httpClient.GetAsync("/api/parking/status");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<ParkingStatusResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root?.Status ?? new List<ParkingSpot>();
        }
    }

    public class ParkingStatusResponse
    {
        public bool Success { get; set; }
        public List<ParkingSpot> Status { get; set; }
    }
}
