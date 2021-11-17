using IkasaBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api.helper
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenGenerator(IConfiguration configuration,
            UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(AppUser appUser)
        {
            // payload part of jwt holds the claims
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id), // nameidentifier is used to get th Id
                new Claim(ClaimTypes.Email, appUser.Email)
               // new Claim(ClaimTypes.Role, appUser.)
            };

            var roles = await _userManager.GetRolesAsync(appUser);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"])
                );

            var token = new JwtSecurityToken(
                audience: _configuration["JWTSettings:Audience"],
                issuer: _configuration["JWTSettings:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
