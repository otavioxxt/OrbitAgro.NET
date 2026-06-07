using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("TB_PRODUTOR")]
public class ProdutorEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string Telefone { get; set; } = string.Empty;

    [StringLength(14)]
    public string Cpf { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public ICollection<AreaCultivoEntity>? Areas { get; set; }
}
