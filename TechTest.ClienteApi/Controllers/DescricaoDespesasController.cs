using ClienteApi.Data;
using ClienteApi.Extensions;
using ClienteApi.Models;
using ClienteApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClienteApi.ViewModels.Acomodacao;
using ClienteApi.ViewModels.DescricaoDespesa;

namespace ClienteApi.Controllers
{
    [ApiController]
    [Route("v1/descricaodespesas")]
    public class DescricaoDespesasController : HomeController
    {
        [HttpGet]
        public IActionResult GetAsync([FromServices] IMemoryCache cache, [FromServices] ClienteDbContext context)
        {
            try
            {
                var despesas = cache.GetOrCreate("DescricaoDespesasCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetAcomodacoes(context);
                });

                return Ok(new ResultViewModel<List<DescricaoDespesa>>(despesas));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<DescricaoDespesa>>("05X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ClienteDbContext context)
        {
            try
            {
                var despesa = await context.DescricaoDespesas.FirstOrDefaultAsync(x => x.Id == id);

                if (despesa == null) return NotFound(new ResultViewModel<DescricaoDespesa>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<DescricaoDespesa>(despesa));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<DescricaoDespesa>("05X02 - Falha interna no servidor"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EditDescricaoDespesaViewModel model, [FromServices] ClienteDbContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<DescricaoDespesa>(ModelState.GetErrors()));

            try
            {
                var tpAccommodation = new DescricaoDespesa
                {
                    DescricaoTipoDeDepesa = model.DescricaoTipoDeDepesa,
                    Anotacoes = model.Anotacoes.ToLower()
                };
                await context.DescricaoDespesas.AddAsync(tpAccommodation);
                await context.SaveChangesAsync();

                return Created($"v1/descricaodespesas/{tpAccommodation.Id}",
                    new ResultViewModel<DescricaoDespesa>(tpAccommodation));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<DescricaoDespesa>($"05X03 - Falha interna no servidor - {ex.Message}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Acomodacao>("05X04 - Falha interna no servidor"));
            }
        }

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

        private static List<DescricaoDespesa> GetAcomodacoes(ClienteDbContext context) => context.DescricaoDespesas.ToList();
    }
}