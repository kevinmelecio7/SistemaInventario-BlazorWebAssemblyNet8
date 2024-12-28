using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Responses;
using System.Net.Http.Json;

namespace AppInvWebClient.ClientServices
{
    public class AccountService : IAccount
    {
        private readonly HttpClient httpClient;
        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private const string BaseUrl = "api/account";

        private static bool CheckIfUnauthorized(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return true;
            }
            else return false;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var response = await httpClient.PostAsJsonAsync($"{BaseUrl}/login", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            else
            {
                // Puedes leer el contenido como texto para ver el mensaje de error
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {errorMessage}");
            }
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var response = await httpClient.PostAsJsonAsync($"{BaseUrl}/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }

        public async Task<LoginResponse> LogoutAsync()
        {
            var response = await httpClient.PostAsync($"{BaseUrl}/logout", null);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }
    }
}
