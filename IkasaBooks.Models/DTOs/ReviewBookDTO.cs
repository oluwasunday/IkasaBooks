using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class ReviewBookDTO
    {
        [Required]
        public string Comments { get; set; }
        [Required]
        public string BookId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
