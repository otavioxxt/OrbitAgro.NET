using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("TB_ALERTA")]
public class AlertaEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string TipoAlerta { get; set; } = string.Empty;

    [StringLength(500)]
    public string Observacao { get; set; } = string.Empty;

    public DateTime DataAlerta { get; set; } = DateTime.Now;

    [StringLength(50)]
    public string StatusAlerta { get; set; } = string.Empty;

    [ForeignKey(nameof(AreaCultivoEntity))]
    public int AreaCultivoId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public AreaCultivoEntity? AreaCultivo { get; set; }
}
