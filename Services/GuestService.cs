using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace appControlAccess1.Services
{
    public static class GuestService
    {
        public static async Task<bool> RegisterGuestAsync(string name, string uid)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(ApiConfig.BaseUrl);

            var data = new
            {
                name = name,
                rfid_uid = uid
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/access/register-guest", content);

            return response.IsSuccessStatusCode;
        }
    }
}
