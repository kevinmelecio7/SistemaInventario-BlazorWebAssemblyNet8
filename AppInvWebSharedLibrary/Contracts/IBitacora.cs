using AppInvWebSharedLibrary.DTOs.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.Contracts
{
    public interface IBitacora
    {
        Task InsertBitacoraAsync(BitacoraDTO model);
    }
}
