namespace AngularUrlShortener.Models.EF
{
    public class AppSeedData
    {
        private static Settings _defaultAppSettings = new Settings
        {
            Hostname = "https://localhost",
            AllowSignup = true,
            AllowAnonymousLinkCreation = true
        };

        private static List<Link> _seedLinks = new List<Link>()
        {
            new Link
            {
                Url = "https://website.com",
                ShortUrl = "afe2v3"
            },
            new Link
            {
                Url = "https://website.com/about",
                ShortUrl = "34fsf5"
            },
            new Link
            {
                Url = "https://website.com/signup",
                ShortUrl = "asf4ss"
            },
        };

        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Settings
            Settings settings = context.Settings.FirstOrDefault();
            if (settings == null)
            {
                context.Settings.Add(_defaultAppSettings);
            }

            // Links
            if (context.Links.Count() == 0)
            {
                foreach (Link link in _seedLinks)
                {
                    context.Links.Add(link);
                }
            }

            context.SaveChanges();
        }
    }
}
