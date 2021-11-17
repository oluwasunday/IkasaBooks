using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Models
{
    public class AppUser : IdentityUser
    {
        public override string Id { get; set; }
        [Required(ErrorMessage = "Enter full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public override string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter email address")]
        public override string Email { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
    }
}
