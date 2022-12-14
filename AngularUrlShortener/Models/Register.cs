using System.ComponentModel.DataAnnotations;

namespace AngularUrlShortener.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Password2 { get; set; }
    }
}
