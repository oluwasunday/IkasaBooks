using AutoMapper;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api.helper
{
    class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<AddUserDTO,AppUser>();

            //.ForMember used to do custom mapping where fields in both source and destination are not same
            // example: there is no Age and Name properties in AppUser
            // so we do custom mapping
        }
    }
}
