using ClienteApi.Data;
using ClienteApi.Extensions;
using ClienteApi.Models;
using ClienteApi.Services.TypeOfAccommodation;
using ClienteApi.ViewModels;
using ClienteApi.ViewModels.TypeOfAccommodation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteApi.Controllers
{
    [ApiController]
    [Route("v1/TipoDeControlador")]
    public class TypeOfLocationController : BaseController
    {
        [HttpGet("")]
        public IActionResult GetAsync([FromServices] IMemoryCache cache, [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpLocation = cache.GetOrCreate("TypeOfLocationsCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetTypeOfLocations(context);
                });

                return Ok(new ResultViewModel<List<TypeOfLocation>>(tpLocation));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<TypeOfLocation>>("06X01 - Falha interna no servidor"));
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpLocation = await context.TypeOfLocations.FirstOrDefaultAsync(x => x.Id == id);

                if (tpLocation == null) return NotFound(new ResultViewModel<TypeOfLocation>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<TypeOfLocation>(tpLocation));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<TypeOfLocation>("06X02 - Falha interna no servidor"));
            }
        }
        
        private static List<TypeOfLocation> GetTypeOfLocations(ClienteDbContext context) 
            => context.TypeOfLocations.ToList();

    }
}