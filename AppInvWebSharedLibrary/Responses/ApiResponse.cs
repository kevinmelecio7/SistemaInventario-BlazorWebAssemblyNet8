using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.Responses
{
    public class ApiResponse<T>
    {
        public string Mensaje { get; set; }
        public T Response { get; set; }
    }
}
