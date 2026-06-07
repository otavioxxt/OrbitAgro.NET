using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonitoramentoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public MonitoramentoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os monitoramentos")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<MonitoramentoEntity>))]
    [SwaggerResponse(204, "Nenhum monitoramento encontrado")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.Monitoramento.ToListAsync();

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
    [SwaggerOperation(Summary = "Busca monitoramento por ID")]
    [SwaggerResponse(200, "Monitoramento encontrado", typeof(MonitoramentoEntity))]
    [SwaggerResponse(404, "Monitoramento não encontrado")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.Monitoramento.FindAsync(id);

            if (result is null)
                return NotFound(new { mensagem = "Monitoramento não encontrado." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("area/{areaId}")]
    [SwaggerOperation(Summary = "Cadastra um novo monitoramento para uma área")]
    [SwaggerResponse(200, "Monitoramento cadastrado com sucesso", typeof(MonitoramentoEntity))]
    [SwaggerResponse(404, "Área de cultivo não encontrada")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(int areaId, [FromBody] MonitoramentoEntity model)
    {
        try
        {
            var area = await _context.AreaCultivo.FindAsync(areaId);

            if (area is null)
                return NotFound(new { mensagem = "Área de cultivo não encontrada." });

            var monitoramento = new MonitoramentoEntity
            {
                IndiceNdvi = model.IndiceNdvi,
                NdviAnterior = model.NdviAnterior,
                UmidadeSolo = model.UmidadeSolo,
                TemperaturaSolo = model.TemperaturaSolo,
                DataLeitura = DateTime.Now,
                AreaCultivoId = areaId
            };

            _context.Monitoramento.Add(monitoramento);
            await _context.SaveChangesAsync();

            return Ok(monitoramento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um monitoramento")]
    [SwaggerResponse(200, "Monitoramento atualizado com sucesso", typeof(MonitoramentoEntity))]
    [SwaggerResponse(404, "Monitoramento não encontrado")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Put(int id, [FromBody] MonitoramentoEntity model)
    {
        try
        {
            var monitoramento = await _context.Monitoramento.FindAsync(id);

            if (monitoramento is null)
                return NotFound(new { mensagem = "Monitoramento não encontrado." });

            monitoramento.IndiceNdvi = model.IndiceNdvi;
            monitoramento.NdviAnterior = model.NdviAnterior;
            monitoramento.UmidadeSolo = model.UmidadeSolo;
            monitoramento.TemperaturaSolo = model.TemperaturaSolo;

            _context.Monitoramento.Update(monitoramento);
            await _context.SaveChangesAsync();

            return Ok(monitoramento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove um monitoramento")]
    [SwaggerResponse(204, "Monitoramento removido com sucesso")]
    [SwaggerResponse(404, "Monitoramento não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var monitoramento = await _context.Monitoramento.FindAsync(id);

            if (monitoramento is null)
                return NotFound(new { mensagem = "Monitoramento não encontrado." });

            _context.Monitoramento.Remove(monitoramento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
