using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.DTOs
{
    public class UserDTO
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? correo { get; set; }
        public string? rol { get; set; }
    }
}
