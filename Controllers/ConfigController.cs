using ASPNET5Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

//using ASPNET5Demo1.Models;

namespace ASPNET5Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IOptions<JwtSettings> _JwtSettings;

        public ConfigController(IOptions<JwtSettings> JwtSettings)
        {
            _JwtSettings = JwtSettings;
        }


        [HttpGet("GetJwtSettings")]
        public ActionResult<JwtSettings> GetJwtSettings()
        {
            return _JwtSettings.Value;
        }
    }
}