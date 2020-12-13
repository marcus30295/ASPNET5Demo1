using ASPNET5Demo1.Helper;
using ASPNET5Demo1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNET5Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly JwtHelper _jwtHelper;

        public AccountController(ILogger<AccountController> logger, JwtHelper jwtHelper)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        [Route("~/Login")]
        public LonginResult Login(LoginModel model)
        {
            return new LonginResult
            {
                token =
            _jwtHelper.GenerateToken(model.UserName, 20)
            };
        }

        [HttpPost]
        [Route("~/RefreshToken")]
        [Authorize]
        public LonginResult RefreshToken()
        {
            return new LonginResult
            {
                token =
                    _jwtHelper.GenerateToken(User.Identity.Name, 20)
            };
        }
    }
}