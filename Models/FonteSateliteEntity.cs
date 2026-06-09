using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("T_TB_FONTE_SATELITE")]
public class FonteSateliteEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string NomeFonte { get; set; } = string.Empty;

    public bool Ativo { get; set; }

    public ICollection<MonitoramentoEntity>? Monitoramentos { get; set; }
}