namespace AngularUrlShortener.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string? Hostname { get; set; }
        public bool? AllowSignup { get; set; }
        public bool? AllowAnonymousLinkCreation { get; set; }
    }
}
