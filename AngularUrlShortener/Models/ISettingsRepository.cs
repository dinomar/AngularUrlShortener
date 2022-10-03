namespace AngularUrlShortener.Models
{
    public interface ISettingsRepository
    {
        Settings Get();
        Settings Update(Settings settings);
    }
}
