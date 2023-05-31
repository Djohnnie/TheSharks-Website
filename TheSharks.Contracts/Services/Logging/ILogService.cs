using TheSharks.Domain.Entities;

namespace TheSharks.Contracts.Services.Logging;

public interface ILogService
{
    Task RecordLog(LogIdentifier identifier, string message);
    Task RecordLog(LogIdentifier identifier, string user, string message);
    Task RecordLog<T>(LogIdentifier identifier, string message, T data);
}