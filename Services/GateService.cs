using System.Net.Http;
using System.Threading.Tasks;

namespace appControlAccess1.Services
{
    public class GateService
    {
        private readonly HttpClient _httpClient;

        public GateService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> OpenGateAsync(int gateNumber)
        {
            var url = $"{ApiConfig.BaseUrl}/api/pluma{(gateNumber == 2 ? "2" : "")}/open";
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CloseGateAsync(int gateNumber)
        {
            var url = $"{ApiConfig.BaseUrl}/api/pluma{(gateNumber == 2 ? "2" : "")}/close";
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetGateStatusAsync(int gateNumber)
        {
            var url = $"{ApiConfig.BaseUrl}/api/pluma{(gateNumber == 2 ? "2" : "")}/status";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return string.Empty;
        }
    }
}
