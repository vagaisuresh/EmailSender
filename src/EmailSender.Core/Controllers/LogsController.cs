using EmailSender.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly ILoggerService _logger;

    public LogsController(ILoggerService logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> GetLogs()
    {
        _logger.LogDebug("Here is debug message from the controller.");
        _logger.LogError("Here is error message from the controller.");
        _logger.LogInfo("Here is info message from the controller.");
        _logger.LogWarning("Here is warn message from the controller.");

        return new string[] { "value1", "value2" };
    }
}