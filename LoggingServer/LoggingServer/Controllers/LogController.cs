using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingServer.Controllers
{
    /// <summary>
    /// For testing purpose
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger) => _logger = logger;

        [HttpPost]
        public ActionResult Get()
        {
            _logger.LogInformation("Information Testing");
            _logger.LogWarning("Warning Testing");
            _logger.LogError("Error Testing");
            return Ok();
        }
    }
}