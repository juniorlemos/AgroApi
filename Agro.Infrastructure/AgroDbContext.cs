using Agro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure
{
    public class AgroDbContext : DbContext
    {
        public AgroDbContext(DbContextOptions options) : base(options) { }
        public DbSet<SaidaAnimais> SaidasAnimais { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<UnidadeExploracao> UnidadesExploracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgroDbContext).Assembly);
        }
    }
}
