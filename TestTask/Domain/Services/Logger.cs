using Microsoft.EntityFrameworkCore;
using TestTask.DataAccess;
using TestTask.Domain.Models;

namespace TestTask.Domain.Services;

public interface ILogger
{
    Task Log(string message, LogType type = LogType.Info);
    Task<IEnumerable<LogRecord>> GetLogs();
}

public class Logger(AppDbContext dbContext) : ILogger
{
    public async Task<IEnumerable<LogRecord>> GetLogs()
    {
        return await dbContext.LogRecords.AsNoTracking().ToArrayAsync();
    }

    public async Task Log(string message, LogType type = LogType.Info)
    {
        if(string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));

        await dbContext.LogRecords.AddAsync(new LogRecord
        {
            Type = type,
            Action = message,
            DateTime = DateTime.Now
        });

        await dbContext.SaveChangesAsync();
    }
}
