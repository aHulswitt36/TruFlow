namespace Infrastructure.Settings
{
    public class UsgsSettings
    {
        public string BaseUrl { get; set; }
    }

    public class BaseSettings
    {
        public UsgsSettings UsgsSettings { get; set; }
    }
}
