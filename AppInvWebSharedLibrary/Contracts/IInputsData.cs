using AppInvWebSharedLibrary.DTOs.Excel;
using AppInvWebSharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.Contracts
{
    public interface IInputsData
    {
        Task<List<PeriodoDTO>> GetPeriodoAsync();
        Task InsertPeriodoAsync(PeriodoDTO model);
        Task UpdatePeriodoAsync(PeriodoDTO model);

        Task<List<StorageBinDTO>> GetStorageAsync(int periodo);
        Task InsertStorageAsync(List<StorageBinDTO> list);
        Task UpdateStorageAsync(StorageBinDTO obj);
        Task DeleteStorageAsync(List<int> list);

        Task<List<MasterDataDTO>> GetMasterDataAsync(int periodo);
        Task InsertMasterDataAsync(List<MasterDataDTO> list);
        Task UpdateMasterDataAsync(MasterDataDTO obj);
        Task DeleteMasterDataAsync(List<int> list);

        Task<List<InitialLoadDTO>> GetInitialLoadAsync(int periodo);
        Task<List<InitialLoadDTO>> GetInitialLoadFirstAsync(int periodo);
        Task<List<InitialLoadDTO>> GetInitialMaterialLoadAsync(int periodo, string material);
        Task<List<InitialLoadDTO>> GetInitialLoadMaterialxStorageAsync(int periodo, string material, string storage);
        Task InsertInitialLoadAsync(List<InitialLoadDTO> list);
        Task DeleteInitialLoadAsync(List<int> list);
        Task UpdateInitialLoadFolioAsync();
        Task UpdateInitialLoadEstadoAsync(InitialLoadDTO obj);


        Task InsertReporteAsync(ReporteDTO obj);
        Task<List<ReporteDTO>> GetReportePorPeriodoAsync(string periodo);
        Task UpdateReporteTodosCamposAsync(ReporteDTO obj);
        Task UpdateReporteAsync(ReporteDTO obj);
        Task DeleteReporteAsync(ReporteDTO obj);

        Task<List<ReporteDTO>> GetInitialLoadPendientesAsync(string periodo);


    }
}
