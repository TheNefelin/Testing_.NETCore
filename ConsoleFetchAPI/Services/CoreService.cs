using ConsoleFetchAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleFetchAPI.Services
{
    internal class CoreService
    {
        private readonly string _apiKey = "ESMERILEMELO";
        private readonly string _apiServer = "https://artema.bsite.net";
        private readonly HttpClient _httpClient;

        public CoreService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Result<CoreIV>> CoreRegister(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreIV, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/register", data);
        }

        public async Task<Result<CoreIV>> CoreLogin(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreIV, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/login", data);
        }

        public async Task<Result<List<CoreData>>> CoreGetAll(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<List<CoreData>, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/get-all", data);
        }

        public async Task<Result<CoreData>> CoreGetById(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreData, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/get-byid", data);
        }

        public async Task<Result<CoreData>> CoreInsert(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreData, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/insert", data);
        }

        public async Task<Result<CoreIV>> CoreUpdate(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreIV, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/update", data);
        }

        public async Task<Result<CoreIV>> CoreDelete(string token, CoreRequest<CoreData> data)
        {
            return await RequestApiQuery<CoreIV, CoreRequest<CoreData>>(token, $"{_apiServer}/api/core/delete", data);
        }

        public async Task<Result<TResponse>> RequestApiQuery<TResponse, TRequest>(string token, string uri, TRequest obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                if (!_httpClient.DefaultRequestHeaders.Contains("ApiKey"))
                    _httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

                var response = await _httpClient.PatchAsync(uri, content);
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
