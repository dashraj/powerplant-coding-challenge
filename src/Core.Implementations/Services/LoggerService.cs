using Core.Services;
using Microsoft.Extensions.Logging;

namespace Core.Implementations.Services
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public int LogException(Exception exception, string requestSummary)
        {
            int errorReference = GenerateReference();
            _logger.LogError(exception, $"Error Refference: {errorReference} \n Request Summary : {requestSummary} \n Message:{exception.Message}");
            return errorReference;
        }

        private static int GenerateReference()
        {
            byte[] guid = Guid.NewGuid().ToByteArray();
            int reference = BitConverter.ToInt32(guid, 0);
            return reference > 0 ? reference : -reference;
        }

    }
}
