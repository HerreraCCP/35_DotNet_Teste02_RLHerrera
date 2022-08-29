using ClienteApi.Data;
using ClienteApi.Extensions;
using ClienteApi.Models;
using ClienteApi.ViewModels;
using ClienteApi.ViewModels.Acomodacao;
using Microsoft.AspNetCore.Authorization;
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
    [Route("[controller]")]
    public class AcomodacoesController : HomeController
    {
        [HttpGet("v1/acomodacoes")]
        public IActionResult GetAsync([FromServices] IMemoryCache cache, [FromServices] ClienteDbContext context)
        {
            try
            {
                var acomodacoes = cache.GetOrCreate("AcomodacaoCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetAcomodacoes(context);
                });

                return Ok(new ResultViewModel<List<Acomodacao>>(acomodacoes));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Acomodacao>>("05X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/acomodacoes/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var acomodacao = await context.Acomodacoes.FirstOrDefaultAsync(x => x.Id == id);

                if (acomodacao == null) return NotFound(new ResultViewModel<Acomodacao>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<Acomodacao>(acomodacao));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X02 - Falha interna no servidor"));
            }
        }

        [Authorize(Roles = "admin, regular, user")]
        [HttpPost("v1/acomodacoes")]
        public async Task<IActionResult> PostAsync([FromBody] EditAcomodacaoViewModel model,
            [FromServices] ClienteDbContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Acomodacao>(ModelState.GetErrors()));

            try
            {
                var tpAccommodation = new Acomodacao
                {
                    DescricaoDaAcomodacao = model.DescricaoDaAcomodacao,
                    Anotacoes = model.Anotacoes.ToLower(),
                };
                await context.Acomodacoes.AddAsync(tpAccommodation);
                await context.SaveChangesAsync();

                return Created($"v1/acomodacoes/{tpAccommodation.Id}",
                    new ResultViewModel<Acomodacao>(tpAccommodation));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,
                    new ResultViewModel<Acomodacao>($"05X03 - Falha interna no servidor - {ex.Message}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X04 - Falha interna no servidor"));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("v1/acomodacoes/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditAcomodacaoViewModel model,
            [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpAccommodations = await context.Acomodacoes.FirstOrDefaultAsync(x => x.Id == id);
                if (tpAccommodations == null)
                    return NotFound(new ResultViewModel<Acomodacao>("05X07 -Conteúdo não encontrado"));

                tpAccommodations.DescricaoDaAcomodacao = model.DescricaoDaAcomodacao;
                tpAccommodations.Anotacoes = model.Anotacoes;

                context.Acomodacoes.Update(tpAccommodations);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Acomodacao>(tpAccommodations));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X05 - Falha interna no servidor"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X06 - Falha interna no servidor"));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("v1/acomodacoes/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var tpAccommodations = await context.Acomodacoes.FirstOrDefaultAsync(x => x.Id == id);

                if (tpAccommodations == null)
                    return NotFound(new ResultViewModel<Acomodacao>("05X08 -Conteúdo não encontrado"));

                context.Acomodacoes.Remove(tpAccommodations);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Acomodacao>(tpAccommodations));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X09 - Falha interna no servidor"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X10 - Falha interna no servidor"));
            }
        }

        private static List<Acomodacao> GetAcomodacoes(ClienteDbContext context) => context.Acomodacoes.ToList();
    }
}