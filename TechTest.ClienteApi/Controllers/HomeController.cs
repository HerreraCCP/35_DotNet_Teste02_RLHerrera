using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClienteApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : BaseController
    {
        [HttpGet("")]
        public IActionResult Get([FromServices] IConfiguration config) =>
            Ok(new {environmnet = config.GetValue<string>("Env")});
    }
}