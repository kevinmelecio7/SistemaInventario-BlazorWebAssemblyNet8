using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs.Excel
{
    public class BitacoraDTO
    {
        public int Id { get; set; }
        public string? vista { get; set; }
        public string? accion { get; set; }
        public string? tipo { get; set; }
        public string? descripcion { get; set; }
        public string? usuario { get; set; }
    }
}
