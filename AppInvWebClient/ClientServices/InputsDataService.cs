using AppInvWebSharedLibrary.DTOs.Excel;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Responses;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace AppInvWebClient.ClientServices
{
    public class InputsDataService
    {
        private readonly BitacoraService bitacoraService;

        private readonly HttpClient httpClient;
        public InputsDataService(HttpClient httpClient, BitacoraService bitacoraService)
        {
            this.httpClient = httpClient;
            this.bitacoraService = bitacoraService;
        }
        private const string BaseUrl = "api/InputsData";

        public async Task<List<PeriodoDTO>> GetPeriodoAsync()
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetPeriodo";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<PeriodoDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<PeriodoDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetPeriodoAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<PeriodoDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetPeriodoAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<PeriodoDTO>();
            }
        }
        public async Task<string> InsertPeriodoAsync(PeriodoDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertPeriodo";
                var response = await httpClient.PostAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertPeriodoAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertPeriodoAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdatePeriodoAsync(PeriodoDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdatePeriodo";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdatePeriodoAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdatePeriodoAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }


        public async Task<List<StorageBinDTO>> GetStorageAsync(int periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetStorage?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<StorageBinDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<StorageBinDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetStorageAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<StorageBinDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<StorageBinDTO>();
            }
        }
        public async Task<string> InsertStorageAsync(List<StorageBinDTO> list)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertStorage";
                var response = await httpClient.PostAsJsonAsync(apiURL, list);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertStorageAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdateStorageAsync(StorageBinDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateStorage";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateStorageAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> DeleteStorageAsync(List<StorageBinDTO> list)
        {
            try
            {
                var ids = list.Select(x => x.id);
                //var jsonBody = JsonSerializer.Serialize(ids);
                //Console.WriteLine(jsonBody);
                var content = new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");

                string apiURL = $"{BaseUrl}/DeleteStorage";

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
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteStorageAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<List<MasterDataDTO>> GetMasterDataAsync(int periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetMaterial?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<MasterDataDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<MasterDataDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetMasterDataAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<MasterDataDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetMasterDataAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<MasterDataDTO>();
            }
        }
        public async Task<string> InsertMasterDataAsync(List<MasterDataDTO> list)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertMaterial";
                var response = await httpClient.PostAsJsonAsync(apiURL, list);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertMasterDataAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertMasterDataAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdateMasterDataAsync(MasterDataDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateMaterial";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateMasterDataAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateMasterDataAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> DeleteMasterDataAsync(List<MasterDataDTO> list)
        {
            try
            {
                var ids = list.Select(x => x.id_material);
                //var jsonBody = JsonSerializer.Serialize(ids);
                //Console.WriteLine(jsonBody);
                var content = new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");

                string apiURL = $"{BaseUrl}/DeleteMaterial";

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
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteMasterDataAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<List<InitialLoadDTO>> GetInitialLoadAsync(int periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetInitialLoad?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<InitialLoadDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<InitialLoadDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
        }

        public async Task<List<InitialLoadDTO>> GetInitialLoadFirstAsync(int periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetInitialLoadFirst?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<InitialLoadDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<InitialLoadDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadFirstAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadFirstAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
        }
        public async Task<List<InitialLoadDTO>> GetInitialMaterialLoadAsync(int periodo, string material)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetInitialMaterialLoad?periodo={periodo}&material={material}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<InitialLoadDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<InitialLoadDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialMaterialLoadAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialMaterialLoadAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
        }

        public async Task<List<InitialLoadDTO>> GetInitialLoadMaterialxStorageAsync(int periodo, string material, string storage)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetInitialLoadMaterialxStorage?periodo={periodo}&material={material}&storage={storage}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<InitialLoadDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<InitialLoadDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadMaterialxStorageAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadMaterialxStorageAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<InitialLoadDTO>();
            }
        }
        public async Task<string> InsertInitialLoadAsync(List<InitialLoadDTO> list)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertInitialLoad";
                var response = await httpClient.PostAsJsonAsync(apiURL, list);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertInitialLoadAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertInitialLoadAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> DeleteInitialLoadAsync(List<InitialLoadDTO> list)
        {
            try
            {
                var ids = list.Select(x => x.id_saldos);
                //var jsonBody = JsonSerializer.Serialize(ids);
                //Console.WriteLine(jsonBody);
                var content = new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");

                string apiURL = $"{BaseUrl}/DeleteInitialLoad";

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
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteInitialLoadAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsData", accion = "DeleteInitialLoadAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdateInitialLoadFolioAsync()
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateInitialLoadFolio";
                var response = await httpClient.PutAsync(apiURL, null);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateInitialLoadFolioAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateInitialLoadFolioAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdateInitialLoadEstadoAsync(InitialLoadDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateInitialLoadEstado";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateInitialLoadEstadoAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateInitialLoadEstadoAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<string> InsertReporteAsync(ReporteDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/InsertReporte";
                var response = await httpClient.PostAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertReporteAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "InsertReporteAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<List<ReporteDTO>> GetReportePorPeriodoAsync(string periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetReportePorPeriodo?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<ReporteDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<ReporteDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetReportePorPeriodoAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<ReporteDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetReportePorPeriodoAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<ReporteDTO>();
            }
        }

        public async Task<string> UpdateReporteTodosCamposAsync(ReporteDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateReporteTodosCampos";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateReporteTodosCamposAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateReporteTodosCamposAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> UpdateReporteAsync(ReporteDTO model)
        {
            try
            {
                string apiURL = $"{BaseUrl}/UpdateReporte";
                var response = await httpClient.PutAsJsonAsync(apiURL, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse!.Response == null && apiResponse!.Mensaje == "ok")
                    {
                        return apiResponse.Mensaje;
                    }
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateReporteAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "UpdateReporteAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }
        public async Task<string> DeleteReporteAsync(ReporteDTO model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                string apiURL = $"{BaseUrl}/DeleteReporte";

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
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "DeleteReporteAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "DeleteReporteAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return "Error";
            }
        }

        public async Task<List<ReporteDTO>> GetInitialLoadPendientesAsync(string periodo)
        {
            try
            {
                string apiURL = $"{BaseUrl}/GetInitialLoadPendientes?periodo={periodo}";
                var response = await httpClient.GetFromJsonAsync<ApiResponse<List<ReporteDTO>>>(apiURL);
                if (response != null && response.Mensaje == "ok")
                {
                    return response.Response ?? new List<ReporteDTO>();
                }
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadPendientesAsync", tipo = "ERROR", descripcion = response.ToString(), usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<ReporteDTO>();
            }
            catch (Exception ex)
            {
                BitacoraDTO bitacora = new BitacoraDTO { vista = "InputsDataService", accion = "GetInitialLoadPendientesAsync", tipo = "ERROR", descripcion = ex.Message, usuario = "0" };
                await bitacoraService.InsertBitacoraAsync(bitacora);
                return new List<ReporteDTO>();
            }
        }

    }
}
