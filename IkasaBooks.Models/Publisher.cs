using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkasaBooks.Models
{
    public class Publisher
    {
        public string Id { get; set; }
        [Required]
        public string PublisherName { get; set; }
        [Required]
        [Range(4,4, ErrorMessage = "Invalid year")]
        public int YearPublished { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
