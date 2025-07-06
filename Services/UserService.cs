using appControlAccess1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace appControlAccess1.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiConfig.BaseUrl}/api/users");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var wrapper = JsonConvert.DeserializeObject<UserListResponse>(json);
                return wrapper?.data ?? new List<User>();
            }
            return new List<User>();
        }


        public async Task<bool> CreateUserAsync(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ApiConfig.BaseUrl}/api/users", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiConfig.BaseUrl}/api/users/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiConfig.BaseUrl}/api/users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
