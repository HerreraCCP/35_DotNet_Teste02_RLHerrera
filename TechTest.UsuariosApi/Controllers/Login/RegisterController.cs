using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : BaseController
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService) => _registerService = registerService;

        [HttpPost]
        public IActionResult UserRegistration(CreateUserDto createDto)
        {
            var result = _registerService.UserRegistration(createDto);
            return result.IsFailed ? StatusCode(500) : Ok(result.Successes);
        }

        [HttpGet("/Active")]
        public IActionResult ActivateUserAccount([FromQuery] ActivateAccountRequest request)
        {
            var resultado = _registerService.ActivateUserAccount(request);
            return resultado.IsFailed ? StatusCode(500) : Ok(resultado.Successes);
        }
    }
}