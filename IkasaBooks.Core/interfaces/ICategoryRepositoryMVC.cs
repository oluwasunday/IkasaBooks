using IkasaBooks.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public interface ICategoryRepositoryMVC
    {
        Task<IEnumerable<CategoryNamesDTO>> GetCategoryNames();
    }
}