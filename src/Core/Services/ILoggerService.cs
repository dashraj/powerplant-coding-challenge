namespace Core.Services
{
    public interface ILoggerService<T>
    {
        void LogInformation(string message);
        int LogException(Exception exception, string requestSummary);
    }
}
