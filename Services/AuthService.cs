using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using appControlAccess1.Models;

namespace appControlAccess1.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiConfig.BaseUrl);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/auth/login", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
