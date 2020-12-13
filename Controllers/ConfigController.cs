using ASPNET5Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

//using ASPNET5Demo1.Models;

namespace ASPNET5Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IOptions<JwtSettings> _JwtSettings;
        private readonly ILogger _logger;
        public ConfigController(IOptions<JwtSettings> JwtSettings,ILogger<ConfigController> logger)
        {
            this._JwtSettings = JwtSettings;
            this._logger = logger;
        }


        [HttpGet("GetJwtSettings")]
        public ActionResult<JwtSettings> GetJwtSettings()
        {
            const int id = 123;
            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogInformation("Information");
             _logger.LogInformation("Test:{id}", id);
            _logger.LogWarning("Warning");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
            return _JwtSettings.Value;
        }
    }
}