using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs.Excel
{
    public class StorageBinDTO
    {
        public int id { get; set; }
        public string? storagebin { get; set; }
        public string? storagetype { get; set; }
        public int fkPeriodo { get; set; }
    }
}
