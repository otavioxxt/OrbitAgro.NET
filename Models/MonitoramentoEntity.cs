using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitAgro.API.Models;

[Table("TB_MONITORAMENTO")]
public class MonitoramentoEntity
{
    [Key]
    public int Id { get; set; }

    public double IndiceNdvi { get; set; }

    public double NdviAnterior { get; set; }

    public double UmidadeSolo { get; set; }

    public double TemperaturaSolo { get; set; }

    public DateTime DataLeitura { get; set; } = DateTime.Now;

    [ForeignKey(nameof(AreaCultivoEntity))]
    public int AreaCultivoId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public AreaCultivoEntity? AreaCultivo { get; set; }

    [ForeignKey(nameof(FonteSateliteEntity))]
    public int FonteSateliteId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public FonteSateliteEntity? FonteSatelite { get; set; }
}