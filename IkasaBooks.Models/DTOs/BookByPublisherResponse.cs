using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class BookByPublisherResponse
    {
       
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string PublisherName { get; set; }
        public DateTime DatePublished { get; set; }


    }
}
