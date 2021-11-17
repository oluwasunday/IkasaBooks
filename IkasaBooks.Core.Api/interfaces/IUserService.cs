using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api
{
    public interface IUserService
    {
        //Task<UserDTO> AddNewUser(AddUserDTO addUser);
        Task<AppUser> AddNewUser(AddUserDTO addUser);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        //Task<IEnumerable<AppUser>> GetAllUsers();
        Task<UserDTO> GetUserByEmail(string email);
        Task<UserDTO> GetUserById(string id);
        Task<UserDTO> GetUserByUsername(string username);
    }
}