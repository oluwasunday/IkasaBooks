using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class BookResponseDTO
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookCategory { get; set; }
        public string Publisher { get; set; }
        public string YearPublished { get; set; }
        public string BookImageUrl { get; set; }
    }
}
