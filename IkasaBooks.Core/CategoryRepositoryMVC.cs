using IkasaBooks.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public class CategoryRepositoryMVC : ICategoryRepositoryMVC
    {
        public async Task<IEnumerable<CategoryNamesDTO>> GetCategoryNames()
        {
            var categories = new List<CategoryNamesDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync("books/categories");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<CategoryNamesDTO>>(result);
                }
            }
            return categories;
        }

    }
}
