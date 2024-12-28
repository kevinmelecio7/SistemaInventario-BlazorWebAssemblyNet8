using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.Responses
{
    public record class RegistrationResponse(bool Flag = false, string Message = null!);
}
