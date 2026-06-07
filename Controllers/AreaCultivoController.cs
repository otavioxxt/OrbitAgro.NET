using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreaCultivoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AreaCultivoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todas as áreas de cultivo")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<AreaCultivoEntity>))]
    [SwaggerResponse(204, "Nenhuma área encontrada")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.AreaCultivo
                .Include(x => x.Monitoramentos)
                .Include(x => x.Alertas)
                .ToListAsync();

            if (!result.Any())
                return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca área de cultivo por ID")]
    [SwaggerResponse(200, "Área encontrada", typeof(AreaCultivoEntity))]
    [SwaggerResponse(404, "Área não encontrada")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.AreaCultivo
                .Include(x => x.Monitoramentos)
                .Include(x => x.Alertas)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return NotFound(new { mensagem = "Área de cultivo não encontrada." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("produtor/{produtorId}")]
    [SwaggerOperation(Summary = "Cadastra uma nova área de cultivo para um produtor")]
    [SwaggerResponse(200, "Área cadastrada com sucesso", typeof(AreaCultivoEntity))]
    [SwaggerResponse(404, "Produtor não encontrado")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(int produtorId, [FromBody] AreaCultivoEntity model)
    {
        try
        {
            var produtor = await _context.Produtor.FindAsync(produtorId);

            if (produtor is null)
                return NotFound(new { mensagem = "Produtor não encontrado." });

            var area = new AreaCultivoEntity
            {
                NomeArea = model.NomeArea,
                Cultura = model.Cultura,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Hectares = model.Hectares,
                ProdutorId = produtorId
            };

            _context.AreaCultivo.Add(area);
            await _context.SaveChangesAsync();

            return Ok(area);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza uma área de cultivo")]
    [SwaggerResponse(200, "Área atualizada com sucesso", typeof(AreaCultivoEntity))]
    [SwaggerResponse(404, "Área não encontrada")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Put(int id, [FromBody] AreaCultivoEntity model)
    {
        try
        {
            var area = await _context.AreaCultivo.FindAsync(id);

            if (area is null)
                return NotFound(new { mensagem = "Área de cultivo não encontrada." });

            area.NomeArea = model.NomeArea;
            area.Cultura = model.Cultura;
            area.Latitude = model.Latitude;
            area.Longitude = model.Longitude;
            area.Hectares = model.Hectares;

            _context.AreaCultivo.Update(area);
            await _context.SaveChangesAsync();

            return Ok(area);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove uma área de cultivo")]
    [SwaggerResponse(204, "Área removida com sucesso")]
    [SwaggerResponse(404, "Área não encontrada")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var area = await _context.AreaCultivo.FindAsync(id);

            if (area is null)
                return NotFound(new { mensagem = "Área de cultivo não encontrada." });

            _context.AreaCultivo.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
