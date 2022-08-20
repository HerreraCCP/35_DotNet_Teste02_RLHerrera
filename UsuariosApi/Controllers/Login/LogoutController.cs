using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService) => _logoutService = logoutService;

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
            var resultado = _logoutService.DeslogaUsuario();
            return resultado.IsFailed ? Unauthorized(resultado.Errors) : Ok(resultado.Successes);
        }
    }
}