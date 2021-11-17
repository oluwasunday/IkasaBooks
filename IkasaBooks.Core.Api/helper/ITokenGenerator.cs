using IkasaBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api.helper
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser appUser);
    }
}
