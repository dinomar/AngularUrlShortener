namespace AngularUrlShortener.Models.EF
{
    public class EFSettingsRepository : ISettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public EFSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Settings Get()
        {
            return _context.Settings.FirstOrDefault();
        }

        public Settings Update(Settings settings)
        {
            if (_context.Settings.Count() == 0)
            {
                _context.Settings.Add(settings);
            }
            else
            {
                Settings entry = _context.Settings.FirstOrDefault();
                if (entry != null)
                {
                    if (!string.IsNullOrEmpty(settings.Hostname))
                    {
                        entry.Hostname = settings.Hostname;
                    }

                    if (settings.AllowSignup.HasValue)
                    {
                        entry.AllowSignup = settings.AllowSignup.Value;
                    }

                    if (settings.AllowAnonymousLinkCreation.HasValue)
                    {
                        entry.AllowAnonymousLinkCreation = settings.AllowAnonymousLinkCreation.Value;
                    }

                    settings = entry;
                }
            }

            _context.SaveChanges();

            return settings;
        }
    }
}
