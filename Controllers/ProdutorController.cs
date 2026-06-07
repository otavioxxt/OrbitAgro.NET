using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Data;
using OrbitAgro.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrbitAgro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutorController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ProdutorController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os produtores")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<ProdutorEntity>))]
    [SwaggerResponse(204, "Nenhum produtor encontrado")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _context.Produtor
                .Include(x => x.Areas)
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
    [SwaggerOperation(Summary = "Busca produtor por ID")]
    [SwaggerResponse(200, "Produtor encontrado", typeof(ProdutorEntity))]
    [SwaggerResponse(404, "Produtor não encontrado")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _context.Produtor
                .Include(x => x.Areas)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return NotFound(new { mensagem = "Produtor não encontrado." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastra um novo produtor")]
    [SwaggerResponse(200, "Produtor cadastrado com sucesso", typeof(ProdutorEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post([FromBody] ProdutorEntity model)
    {
        try
        {
            var produtor = new ProdutorEntity
            {
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone,
                Cpf = model.Cpf,
                DataCadastro = DateTime.Now
            };

            _context.Produtor.Add(produtor);
            await _context.SaveChangesAsync();

            return Ok(produtor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um produtor")]
    [SwaggerResponse(200, "Produtor atualizado com sucesso", typeof(ProdutorEntity))]
    [SwaggerResponse(404, "Produtor não encontrado")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Put(int id, [FromBody] ProdutorEntity model)
    {
        try
        {
            var produtor = await _context.Produtor.FindAsync(id);

            if (produtor is null)
                return NotFound(new { mensagem = "Produtor não encontrado." });

            produtor.Nome = model.Nome;
            produtor.Email = model.Email;
            produtor.Telefone = model.Telefone;
            produtor.Cpf = model.Cpf;

            _context.Produtor.Update(produtor);
            await _context.SaveChangesAsync();

            return Ok(produtor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove um produtor")]
    [SwaggerResponse(204, "Produtor removido com sucesso")]
    [SwaggerResponse(404, "Produtor não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var produtor = await _context.Produtor.FindAsync(id);

            if (produtor is null)
                return NotFound(new { mensagem = "Produtor não encontrado." });

            _context.Produtor.Remove(produtor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
