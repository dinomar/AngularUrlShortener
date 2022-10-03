namespace AngularUrlShortener.Models
{
    public interface ILinksRepository
    {
        IEnumerable<Link> GetAll();
        IEnumerable<Link> GetAll(string userId);
        Link Get(int id);
        Link Get(string shortUrl);
        Link Update(Link link);
        Link Delete(int id);
    }
}
