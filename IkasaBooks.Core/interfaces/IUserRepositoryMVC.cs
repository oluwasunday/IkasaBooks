using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public interface IUserRepositoryMVC
    {
        Task<bool> AddNewUser(AddUserDTO addUser);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByEmail(string email);
        Task<UserDTO> GetUserById(string id);
        Task<UserDTO> GetUserByUsername(string username);
    }
}