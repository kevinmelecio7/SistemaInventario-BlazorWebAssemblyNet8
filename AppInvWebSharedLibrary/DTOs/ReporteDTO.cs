using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs
{
    public class ReporteDTO
    {
        public int id { get; set; }
        public string? folio { get; set; }
        public string? periodo { get; set; }
        public string? estado { get; set; }
        public string? storage_bin { get; set; }
        public string? storage_type { get; set; }
        public string? material_number { get; set; }
        public string? material_descripcion { get; set; }
        public double unit_standard_cost { get; set; }
        public double cantidad_inicial { get; set; }
        public double cantidad_contada { get; set; }
        public double cantidad_segundo { get; set; }
        public double diferencia_cantidad { get; set; }
        public double porcentaje_diferencia { get; set; }
        public double importe_inicial { get; set; }
        public double importe_contada { get; set; }
        public double diferencia_importe { get; set; }
        public double porcentaje_variacion_importe { get; set; }
        public string? usuario { get; set; }
        public DateTime fecha { get; set; }
        public int periodoConsecutivo { get; set;}
    }
}
