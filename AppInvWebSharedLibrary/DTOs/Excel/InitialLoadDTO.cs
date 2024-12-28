using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs.Excel
{
    public class InitialLoadDTO
    {
        public int id_saldos { get; set; }
        public string? plant { get; set; }
        public string? warehouse { get; set; }
        public string? storage_location { get; set; }
        public string? storage_type { get; set; }
        public string? storage_bin { get; set; }
        public string? storage_unit { get; set; }
        public string? material_number { get; set; }
        public string? material_description { get; set; }
        public string? base_unit_of_measure { get; set; }
        public double total_quantity { get; set; }
        public double total_cost { get; set; }
        public string? currency { get; set; }
        public double unit_standard_cost { get; set; }
        public double unrestricted_stock { get; set; }
        public double blocked_stock { get; set; }
        public double quality_inspection { get; set; }
        public double returns_stock { get; set; }
        public double transfer_stock { get; set; }
        public double consignment_stock { get; set; }
        public double consignment_value { get; set; }
        public string? execution_date { get; set; }
        public int fkPeriodo { get; set; }
        public string? folio { get; set; }
        public string? estado { get; set; }

    }
}
