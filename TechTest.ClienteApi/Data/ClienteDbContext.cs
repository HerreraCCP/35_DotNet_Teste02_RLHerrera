using ClienteApi.Data.Configurations;
using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Data
{
    public sealed partial class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options) { }

        public DbSet<Acomodacao> Acomodacoes { get; set; }
        
        public DbSet<Arquivo> Arquivos { get; set; }
        
        public DbSet<DescricaoDespesa> DescricaoDespesas { get; set; }
        
        public DbSet<Despesa> Despesas { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            MapConfig.ConfigMapApplyConfigurations(builder);
            base.OnModelCreating(builder);
        }
    }
}