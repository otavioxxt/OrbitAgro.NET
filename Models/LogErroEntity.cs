using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("T_TB_LOG_ERRO")]
public class LogErroEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string NomeProcedure { get; set; } = string.Empty;

    [Required]
    [StringLength(128)]
    public string NomeUsuario { get; set; } = string.Empty;

    public DateTime DataHoraErro { get; set; } = DateTime.Now;

    public int? CodigoErro { get; set; }

    [StringLength(4000)]
    public string? Mensagem { get; set; }
}