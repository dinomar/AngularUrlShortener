using System.ComponentModel.DataAnnotations;

namespace AngularUrlShortener.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        [Required]
        [MinLength(1)]
        public string Url { get; set; }
        public string? ShortUrl { get; set; }
    }
}
