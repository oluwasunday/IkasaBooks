using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class AddBookResponseDTO
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookCategory { get; set; }
    }
}
