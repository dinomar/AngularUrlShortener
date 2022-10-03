using System.ComponentModel.DataAnnotations;

namespace AngularUrlShortener.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
