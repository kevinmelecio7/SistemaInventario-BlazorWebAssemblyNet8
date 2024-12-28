using AppInvWebSharedLibrary.DTOs.Excel;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Responses;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace AppInvWebClient.ClientServices
{
    public class UserService
    {
        private readonly BitacoraService bitacoraService;

        private readonly HttpClient httpClient;
        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private const string BaseUrl = "api/User";

        public async Task<List<UserDTO>> GetUserAsync()
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetUsers";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<UserDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<UserDTO>();
                }
                return new List<UserDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
                return new List<UserDTO>();
            }

        }

        public async Task<string> UpdateUserAsync(UserDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateUsers";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "UpdateUserAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "UpdateUserAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<string> UpdatePasswordUserAsync(UserDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdatePasswordUsers";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "UpdatePasswordUserAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "UpdatePasswordUserAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<string> DeleteUserAsync(UserDTO model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                string apiURL = $"{BaseUrl}/DeleteUsers";

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(apiURL, UriKind.RelativeOrAbsolute), // Ruta relativa o absoluta según sea necesario
                    Content = content
                };
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "DeleteUserAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "UserService", accion = "DeleteUserAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
    }
}
