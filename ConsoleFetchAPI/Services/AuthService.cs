using ConsoleFetchAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleFetchAPI.Services
{
    internal class AuthService
    {
        private readonly string _apiKey = "ESMERILEMELO";
        private readonly string _apiServer = "https://artema.bsite.net";
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Result<object>> Register(Register register)
        {
            return await RequestApiQuery<object, Register>($"{_apiServer}/api/auth/register", register);
        }

        public async Task<Result<LoggedIn>> Login(Login login)
        {
            return await RequestApiQuery<LoggedIn, Login>($"{_apiServer}/api/auth/login", login);
        }

        public async Task<Result<TResponse>> RequestApiQuery<TResponse, TRequest>(string uri, TRequest obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                if (!_httpClient.DefaultRequestHeaders.Contains("ApiKey"))
                    _httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

                var response = await _httpClient.PostAsync(uri, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();
 
                var responseApi = JsonConvert.DeserializeObject<ResponseApi<TResponse>>(jsonResponse);

                Result<TResponse> result = new()
                {
                    StatusCode = responseApi.StatusCode,
                    Message = responseApi.Message,
                    Data = responseApi.Data
                };

                if (response.IsSuccessStatusCode)
                    result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                return new Result<TResponse>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
