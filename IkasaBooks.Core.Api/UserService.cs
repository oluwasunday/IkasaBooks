using AutoMapper;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AppUser> AddNewUser(AddUserDTO addUser)
        {
            var newAppUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = addUser.FullName,
                Email = addUser.Email,
                Password = addUser.Password,
                PhoneNumber = addUser.PhoneNumber,
                UserName = addUser.Username
                
            };


            IdentityResult result = await _userManager.CreateAsync(newAppUser, addUser.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAppUser, "Regular");
                return newAppUser;
            }

            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }


        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users != null)
            {
                var mapped = _mapper.Map<IEnumerable<UserDTO>>(users);
                return mapped;
            }
            throw new ArgumentException("User not found!");
        }


        public async Task<UserDTO> GetUserById(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
                return _mapper.Map<UserDTO>(appUser);

            throw new KeyNotFoundException("User not found!");
        }


        public async Task<UserDTO> GetUserByEmail(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null)
                return _mapper.Map<UserDTO>(appUser);

            throw new KeyNotFoundException("User not found!");
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser != null)
                return _mapper.Map<UserDTO>(appUser);

            throw new KeyNotFoundException("User not found!");
        }
    }
}
