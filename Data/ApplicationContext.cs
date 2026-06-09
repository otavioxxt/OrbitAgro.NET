using Microsoft.EntityFrameworkCore;
using OrbitAgro.API.Models;

namespace OrbitAgro.API.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<ProdutorEntity> Produtor { get; set; }
    public DbSet<AreaCultivoEntity> AreaCultivo { get; set; }
    public DbSet<MonitoramentoEntity> Monitoramento { get; set; }
    public DbSet<AlertaEntity> Alerta { get; set; }
    public DbSet<FonteSateliteEntity> FonteSatelite { get; set; }
    public DbSet<LogErroEntity> LogErro { get; set; }
}