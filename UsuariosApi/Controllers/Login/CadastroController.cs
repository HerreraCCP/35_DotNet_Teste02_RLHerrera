using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : BaseController
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService) => _cadastroService = cadastroService;

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
        {
            var resultado = _cadastroService.CadastraUsuario(createDto);
            return resultado.IsFailed ? StatusCode(500) : Ok(resultado.Successes);
        }

        [HttpGet("/Ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            var resultado = _cadastroService.AtivaContaUsuario(request);
            return resultado.IsFailed ? StatusCode(500) : Ok(resultado.Successes);
        }
    }
}