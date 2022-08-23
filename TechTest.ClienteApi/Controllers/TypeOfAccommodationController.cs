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
    [Route("v1/tipodeacomodacoes")]
    public class TypeOfAccommodationController : BaseController
    {
        [HttpGet("")]
        public IActionResult GetAsync([FromServices] IMemoryCache cache, [FromServices] ClienteDbContext context)
        {
            try
            {
                var typeOfAccommodations = cache.GetOrCreate("TypeOfAccommodationsCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetAcomodacoes(context);
                });

                return Ok(new ResultViewModel<List<TypeOfAccommodation>>(typeOfAccommodations));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<TypeOfAccommodation>>("05X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var typeOfAccommodation = await context.TypeOfAccommodations.FirstOrDefaultAsync(x => x.Id == id);

                if (typeOfAccommodation == null) return NotFound(new ResultViewModel<TypeOfAccommodation>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<TypeOfAccommodation>(typeOfAccommodation));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X02 - Falha interna no servidor"));
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> PostAsync([FromBody] EditTypeOfAccommodationViewModel model, [FromServices] ClienteDbContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<TypeOfAccommodation>(ModelState.GetErrors()));

            try
            {
                var tpAccommodation = new TypeOfAccommodation
                {
                    DescriptionOfAccommodation = model.DescriptionOfAccommodation,
                    Notes = model.Notes.ToLower(),
                };
                await context.TypeOfAccommodations.AddAsync(tpAccommodation);
                await context.SaveChangesAsync();

                return Created($"v1/TipoDeAcomodacoes/{tpAccommodation.Id}", new ResultViewModel<TypeOfAccommodation>(tpAccommodation));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>($"05X03 - Falha interna no servidor - {ex.Message}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditTypeOfAccommodationViewModel model, [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpAccommodations = await context.TypeOfAccommodations.FirstOrDefaultAsync(x => x.Id == id);
                if (tpAccommodations == null) return NotFound(new ResultViewModel<TypeOfAccommodation>("05X07 -Conteúdo não encontrado"));

                tpAccommodations.DescriptionOfAccommodation = model.DescriptionOfAccommodation;
                tpAccommodations.Notes = model.Notes;

                context.TypeOfAccommodations.Update(tpAccommodations);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<TypeOfAccommodation>(tpAccommodations));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X05 - Falha interna no servidor"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X06 - Falha interna no servidor"));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpAccommodations = await context
                    .TypeOfAccommodations
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tpAccommodations == null)
                    return NotFound(new ResultViewModel<TypeOfAccommodation>("05X08 -Conteúdo não encontrado"));

                context.TypeOfAccommodations.Remove(tpAccommodations);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<TypeOfAccommodation>(tpAccommodations));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X09 - Falha interna no servidor"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<TypeOfAccommodation>("05X10 - Falha interna no servidor"));
            }
        }

        private static List<TypeOfAccommodation> GetAcomodacoes(ClienteDbContext context) => context.TypeOfAccommodations.ToList();
    }
}
