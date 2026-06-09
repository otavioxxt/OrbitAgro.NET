using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FonteSateliteController : ControllerBase
{
    private readonly ApplicationContext _context;

    public FonteSateliteController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todas as fontes de satélite")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<FonteSateliteEntity>))]
    [SwaggerResponse(204, "Nenhuma fonte encontrada")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.FonteSatelite.ToListAsync();

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
    [SwaggerOperation(Summary = "Busca fonte de satélite por ID")]
    [SwaggerResponse(200, "Fonte encontrada", typeof(FonteSateliteEntity))]
    [SwaggerResponse(404, "Fonte não encontrada")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.FonteSatelite.FindAsync(id);

            if (result is null)
                return NotFound(new { mensagem = "Fonte de satélite não encontrada." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastra uma nova fonte de satélite")]
    [SwaggerResponse(200, "Fonte cadastrada com sucesso", typeof(FonteSateliteEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post([FromBody] FonteSateliteEntity model)
    {
        try
        {
            var fonte = new FonteSateliteEntity
            {
                NomeFonte = model.NomeFonte,
                Ativo = model.Ativo
            };

            _context.FonteSatelite.Add(fonte);
            await _context.SaveChangesAsync();

            return Ok(fonte);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza uma fonte de satélite")]
    [SwaggerResponse(200, "Fonte atualizada com sucesso", typeof(FonteSateliteEntity))]
    [SwaggerResponse(404, "Fonte não encontrada")]
    public async Task<IActionResult> Put(int id, [FromBody] FonteSateliteEntity model)
    {
        try
        {
            var fonte = await _context.FonteSatelite.FindAsync(id);

            if (fonte is null)
                return NotFound(new { mensagem = "Fonte de satélite não encontrada." });

            fonte.NomeFonte = model.NomeFonte;
            fonte.Ativo = model.Ativo;

            _context.FonteSatelite.Update(fonte);
            await _context.SaveChangesAsync();

            return Ok(fonte);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove uma fonte de satélite")]
    [SwaggerResponse(204, "Fonte removida com sucesso")]
    [SwaggerResponse(404, "Fonte não encontrada")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var fonte = await _context.FonteSatelite.FindAsync(id);

            if (fonte is null)
                return NotFound(new { mensagem = "Fonte de satélite não encontrada." });

            _context.FonteSatelite.Remove(fonte);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}