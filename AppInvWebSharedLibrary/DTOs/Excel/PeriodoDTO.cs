using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs.Excel
{
    public class PeriodoDTO
    {
        public int id { get; set; }
        public string? periodo { get; set; }
        public int activo { get; set; }
        public DateTime? fecha { get; set; }

    }
}
