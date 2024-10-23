using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Models;
using ILogger = TestTask.Domain.Services.ILogger;

namespace TestTask.Controllers;

[ApiController]
[Route("logs")]
public class LogRecordController(ILogger service) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<LogRecord>> GetOrdersList()
    {
        return await service.GetLogs();
    }
}
