using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogErroController : ControllerBase
{
    private readonly ApplicationContext _context;

    public LogErroController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os logs de erro")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<LogErroEntity>))]
    [SwaggerResponse(204, "Nenhum log encontrado")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.LogErro.ToListAsync();

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
    [SwaggerOperation(Summary = "Busca log de erro por ID")]
    [SwaggerResponse(200, "Log encontrado", typeof(LogErroEntity))]
    [SwaggerResponse(404, "Log não encontrado")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.LogErro.FindAsync(id);

            if (result is null)
                return NotFound(new { mensagem = "Log de erro não encontrado." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Registra um novo log de erro")]
    [SwaggerResponse(200, "Log registrado com sucesso", typeof(LogErroEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post([FromBody] LogErroEntity model)
    {
        try
        {
            var log = new LogErroEntity
            {
                NomeProcedure = model.NomeProcedure,
                NomeUsuario = model.NomeUsuario,
                DataHoraErro = DateTime.Now,
                CodigoErro = model.CodigoErro,
                Mensagem = model.Mensagem
            };

            _context.LogErro.Add(log);
            await _context.SaveChangesAsync();

            return Ok(log);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove um log de erro")]
    [SwaggerResponse(204, "Log removido com sucesso")]
    [SwaggerResponse(404, "Log não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var log = await _context.LogErro.FindAsync(id);

            if (log is null)
                return NotFound(new { mensagem = "Log de erro não encontrado." });

            _context.LogErro.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}