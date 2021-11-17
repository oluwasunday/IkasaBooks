using System;
using System.ComponentModel.DataAnnotations;

namespace IkasaBooks.Models
{
    public class Rating
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string BookId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage="Rating Must be between 1 to 5")]
        public int RatingLevel { get; set; }

        public Book Book { get; set; }
        public AppUser User { get; set; }
    }
}
