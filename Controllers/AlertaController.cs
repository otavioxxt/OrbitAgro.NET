using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertaController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AlertaController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os alertas")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<AlertaEntity>))]
    [SwaggerResponse(204, "Nenhum alerta encontrado")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.Alerta.ToListAsync();

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
    [SwaggerOperation(Summary = "Busca alerta por ID")]
    [SwaggerResponse(200, "Alerta encontrado", typeof(AlertaEntity))]
    [SwaggerResponse(404, "Alerta não encontrado")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.Alerta.FindAsync(id);

            if (result is null)
                return NotFound(new { mensagem = "Alerta não encontrado." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("area/{areaId}")]
    [SwaggerOperation(Summary = "Cadastra um novo alerta para uma área")]
    [SwaggerResponse(200, "Alerta cadastrado com sucesso", typeof(AlertaEntity))]
    [SwaggerResponse(404, "Área de cultivo não encontrada")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(int areaId, [FromBody] AlertaEntity model)
    {
        try
        {
            var area = await _context.AreaCultivo.FindAsync(areaId);

            if (area is null)
                return NotFound(new { mensagem = "Área de cultivo não encontrada." });

            var alerta = new AlertaEntity
            {
                TipoAlerta = model.TipoAlerta,
                Observacao = model.Observacao,
                DataAlerta = DateTime.Now,
                StatusAlerta = model.StatusAlerta,
                AreaCultivoId = areaId
            };

            _context.Alerta.Add(alerta);
            await _context.SaveChangesAsync();

            return Ok(alerta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um alerta")]
    [SwaggerResponse(200, "Alerta atualizado com sucesso", typeof(AlertaEntity))]
    [SwaggerResponse(404, "Alerta não encontrado")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Put(int id, [FromBody] AlertaEntity model)
    {
        try
        {
            var alerta = await _context.Alerta.FindAsync(id);

            if (alerta is null)
                return NotFound(new { mensagem = "Alerta não encontrado." });

            alerta.TipoAlerta = model.TipoAlerta;
            alerta.Observacao = model.Observacao;
            alerta.StatusAlerta = model.StatusAlerta;

            _context.Alerta.Update(alerta);
            await _context.SaveChangesAsync();

            return Ok(alerta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove um alerta")]
    [SwaggerResponse(204, "Alerta removido com sucesso")]
    [SwaggerResponse(404, "Alerta não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var alerta = await _context.Alerta.FindAsync(id);

            if (alerta is null)
                return NotFound(new { mensagem = "Alerta não encontrado." });

            _context.Alerta.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
