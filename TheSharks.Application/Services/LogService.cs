using Microsoft.AspNetCore.Http;
using System.Text.Json;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Services.Logging;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Services;

public class LogService : ILogService
{
    private readonly IRepository<LogEntry> _logsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogService(
        IRepository<LogEntry> logsRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _logsRepository = logsRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task RecordLog(LogIdentifier identifier, string user, string message)
    {
        await AddLogEntry(identifier, user, message, null);
    }

    public async Task RecordLog(LogIdentifier identifier, string message)
    {
        await AddLogEntry(identifier, null, message, null);
    }

    public async Task RecordLog<T>(LogIdentifier identifier, string message, T data)
    {
        await AddLogEntry(identifier, null, message, JsonSerializer.Serialize(data));
    }

    private async Task AddLogEntry(LogIdentifier identifier, string user, string message, string data)
    {
        var entity = new LogEntry
        {
            Id = Guid.NewGuid(),
            Date = DateTimeOffset.UtcNow,
            User = user ?? _httpContextAccessor.HttpContext?.User?.Identity?.Name,
            Identifier = $"{identifier}",
            Message = message,
            Data = data,
        };

        await _logsRepository.Add(entity);
    }
}