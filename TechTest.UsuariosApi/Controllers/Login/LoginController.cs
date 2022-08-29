using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService) => _loginService = loginService;

        [HttpPost]
        public IActionResult LoginUser(LoginRequest request)
        {
            var result = _loginService.UserLogin(request);
            return result.IsFailed ? Unauthorized(result.Errors) : Ok(result.Successes);
        }

        [HttpPost("/Request-Reset")]
        public IActionResult RequestResetUserPassword(ApplyResetRequest request)
        {
            var result = _loginService.RequestResetUserPassword(request);
            return result.IsFailed ? Unauthorized(result.Errors) : Ok(result.Successes);
        }
        
        [HttpPost("/Make-Reset")]
        public IActionResult ResetUserPassword(PerformResetRequest request)
        {
            var result = _loginService.ResetUserPassword(request);
            return result.IsFailed ? Unauthorized(result.Errors) : Ok(result.Successes);
        }
    }
}