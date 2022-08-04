namespace Business.Logs.Interfaces
{
    public interface ILogService
    {
        Task WriteLogAction(string path, string method, string ip, string body = null);
        Task WriteLogException(string function, string message, string? stackTrace);
    }
}
