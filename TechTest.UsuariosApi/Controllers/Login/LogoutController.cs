using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : BaseController
    {
        private readonly LogoutService _logoutService;

        public LogoutController(LogoutService logoutService) => _logoutService = logoutService;

        [HttpPost]
        public IActionResult UserLogout()
        {
            var result = _logoutService.UserLogout();
            return result.IsFailed ? Unauthorized(result.Errors) : Ok(result.Successes);
        }
    }
}