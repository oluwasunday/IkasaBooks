using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
