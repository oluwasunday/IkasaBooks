using AutoMapper;
using IkasaBooks.Core.Api.helper;
using IkasaBooks.Core.Api.interfaces;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        
        

        public AuthenticationRepository(UserManager<AppUser> userNanager, IMapper mapper,
            ITokenGenerator tokenGenerator)
        {
            _userManager = userNanager;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
           
        }

        public async Task<UserDTO> Login(UserLoginDTO loginRequest)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginRequest.Email);
            
            
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    var roles = _userManager.GetRolesAsync(user);
                    var userRole = roles.Result[0];
                    var response = _mapper.Map<UserDTO>(user);
                    response.Role = userRole;

                    response.Token = await _tokenGenerator.GenerateToken(user);
                   
                    return response;
                }
                throw new AccessViolationException("Invalid login details");
            }
            throw new AccessViolationException("Invalid login details");
        }

        public async Task<bool> Role(AppUser user)
        {
            bool isAdmin;
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                isAdmin = true;
                return isAdmin;
            }
            return false;
        }



        public async Task<UserDTO> Register(AddUserDTO registerRequest)
        {
            var user = _mapper.Map<AppUser>(registerRequest);

            IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
            await _userManager.AddToRoleAsync(user, "Regular");

            if (result.Succeeded)
            {
                return _mapper.Map<UserDTO>(user);
            }

            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }
    }
}
