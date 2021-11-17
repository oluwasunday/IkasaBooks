using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryNamesDTO> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<BookResponseDTO> BooksByCategory { get; set; }
        public IEnumerable<Book> IsFeatured { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
