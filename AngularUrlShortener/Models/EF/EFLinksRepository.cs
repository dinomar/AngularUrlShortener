using AngularUrlShortener.Helpers;

namespace AngularUrlShortener.Models.EF
{
    public class EFLinksRepository : ILinksRepository
    {
        private readonly ApplicationDbContext _context;

        public EFLinksRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Link> GetAll()
        {
            return _context.Links;
        }

        public IEnumerable<Link> GetAll(string userId)
        {
            return _context.Links.Where(l => l.UserId == userId);
        }

        public Link Get(int id)
        {
            return _context.Links.FirstOrDefault(l => l.Id == id);
        }

        public Link Get(string shortUrl)
        {
            return _context.Links.FirstOrDefault(l => l.ShortUrl == shortUrl.ToLower());
        }

        public Link Update(Link link)
        {
            if (link.Id == 0)
            {
                link.ShortUrl = RandomCodeGenerator.GetRand(6);
                _context.Links.Add(link);
            }
            else
            {
                Link entry = _context.Links.FirstOrDefault(l => l.Id == link.Id);
                if (entry != null)
                {
                    if (!string.IsNullOrEmpty(link.Url))
                    {
                        entry.Url = link.Url;
                    }

                    if (!string.IsNullOrEmpty(link.ShortUrl))
                    {
                        entry.ShortUrl = link.ShortUrl;
                    }

                    if (!string.IsNullOrEmpty(link.UserId))
                    {
                        entry.UserId = link.UserId;
                    }
                }

                link = entry;
            }

            _context.SaveChanges();

            return link;
        }

        public Link Delete(int id)
        {
            Link entry = _context.Links.FirstOrDefault(l => l.Id == id);
            if (entry != null)
            {
                _context.Links.Remove(entry);
                _context.SaveChanges();
            }

            return entry;
        }
    }
}
