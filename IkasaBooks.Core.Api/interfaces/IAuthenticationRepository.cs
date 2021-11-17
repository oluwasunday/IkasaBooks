using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api.interfaces
{
    public interface IAuthenticationRepository
    {
        Task<UserDTO> Login(UserLoginDTO loginRequest);
        Task<UserDTO> Register(AddUserDTO registerRequest);
        Task<bool> Role(AppUser user);
    }
}
