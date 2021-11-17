using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class RatingDTO
    {
        [Required]
        public string BookId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating Must be between 1 to 5")]
        public int RateLevel { get; set; }
    }
}
