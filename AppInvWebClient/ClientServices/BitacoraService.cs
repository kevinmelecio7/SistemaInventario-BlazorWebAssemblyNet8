using AppInvWebSharedLibrary.DTOs.Excel;
using AppInvWebSharedLibrary.Responses;
using System.Net.Http.Json;

namespace AppInvWebClient.ClientServices
{
    public class BitacoraService
    {
        private readonly HttpClient httpClient;
        public BitacoraService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private const string BaseUrl = "api/Bitacora";

        public async Task<string> InsertBitacoraAsync(BitacoraDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertBitacora";
                var response = await httpClient.PostAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse == null && apiResponse.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }

                return "Error";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Error";
            }
        }
    }
}
