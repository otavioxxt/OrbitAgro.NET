using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("TB_AREA_CULTIVO")]
public class AreaCultivoEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string NomeArea { get; set; } = string.Empty;

    [StringLength(100)]
    public string Cultura { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Hectares { get; set; }

    [ForeignKey(nameof(ProdutorEntity))]
    public int ProdutorId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public ProdutorEntity? Produtor { get; set; }

    public ICollection<MonitoramentoEntity>? Monitoramentos { get; set; }

    public ICollection<AlertaEntity>? Alertas { get; set; }
}
