using AppInvWebSharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInvWebSharedLibrary.Contracts
{
    public interface IUser
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task UpdateUserAsync(UserDTO user);
        Task UpdatePasswordUserAsync(UserDTO user);
        Task DeleteUserAsync(UserDTO user);

    }
}
