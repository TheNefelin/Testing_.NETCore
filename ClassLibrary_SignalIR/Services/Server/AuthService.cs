using ClassLibrary_SignalIR.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace ClassLibrary_SignalIR.Services.Server
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string?> Login(LoginDTO login)
        {
            try
            {
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7168/api/Auth", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseApi = JsonConvert.DeserializeObject<ApiResultDTO>(jsonResponse);

                    Console.Write("Token:");
                    Console.Write(responseApi?.Token);

                    return responseApi?.Token;
                }

                Console.WriteLine("No Estas Autorizado");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Auth Error: Error en la Conexión");
                //Console.WriteLine("Auth Error: " + ex);
                return null;
            }
        }
    }
}
