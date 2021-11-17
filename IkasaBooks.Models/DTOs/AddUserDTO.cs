using System.ComponentModel.DataAnnotations;

namespace IkasaBooks.Models.DTOs
{
    public class AddUserDTO
    {
        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
