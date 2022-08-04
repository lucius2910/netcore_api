namespace Business.Core.Interfaces
{
    public interface ILocalizeServices
    {
        string Get(string module, string screen, string key);
        Task<string> GetAsync(string module, string screen, string key);
    }
}
