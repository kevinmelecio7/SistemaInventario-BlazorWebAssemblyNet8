using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Responses;

namespace AppInvWebSharedLibrary.Contracts
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);
        Task<LoginResponse> LogoutAsync();
    }
}
