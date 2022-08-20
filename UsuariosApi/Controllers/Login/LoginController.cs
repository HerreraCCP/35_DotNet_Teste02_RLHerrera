using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService) => _loginService = loginService;

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request)
        {
            var resultado = _loginService.LogaUsuario(request);
            return resultado.IsFailed ? Unauthorized(resultado.Errors) : Ok(resultado.Successes);
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaResetSenhaUsuario(request);
            return resultado.IsFailed ? Unauthorized(resultado.Errors) : Ok(resultado.Successes);
        }
        
        [HttpPost("/efetua-reset")]
        public IActionResult ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            var resultado = _loginService.ResetaSenhaUsuario(request);
            return resultado.IsFailed ? Unauthorized(resultado.Errors) : Ok(resultado.Successes);
        }
    }
}