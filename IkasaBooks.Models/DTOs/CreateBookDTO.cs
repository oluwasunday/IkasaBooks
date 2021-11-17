using System;
using System.ComponentModel.DataAnnotations;

namespace IkasaBooks.Models.DTOs
{
    public class CreateBookDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public string BookUrl { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public bool IsPdf { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public int YearPublished { get; set; }
    }
}
