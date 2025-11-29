using Accounting_Managment_System_Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace Accounting_Managment_System_Frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
            // set this to your API base url (backend)
            if (_http.BaseAddress == null)
                _http.BaseAddress = new Uri("http://localhost:5175/"); // change port if needed
        }

        public async Task<T?> GetAsync<T>(string relativeUrl)
        {
            var resp = await _http.GetAsync(relativeUrl);
            if (!resp.IsSuccessStatusCode) return default;
            return await resp.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> PostAsync<T>(string relativeUrl, T model)
        {
            var resp = await _http.PostAsJsonAsync(relativeUrl, model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync<T>(string relativeUrl, T model)
        {
            var resp = await _http.PutAsJsonAsync(relativeUrl, model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string relativeUrl)
        {
            var resp = await _http.DeleteAsync(relativeUrl);
            return resp.IsSuccessStatusCode;
        }

        public async Task<LoginResponseViewModel?> LoginAsync(LoginViewModel dto)
        {
            var response = await _http.PostAsJsonAsync("auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponseViewModel>();
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return default;

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }


    }
}
