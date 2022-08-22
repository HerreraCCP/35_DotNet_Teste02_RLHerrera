using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClienteApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get([FromServices]IConfiguration config)
        {
            var env = config.GetValue<string>("Env");
            return Ok(new
            {
                environmnet = env
            });
        }
    }
}