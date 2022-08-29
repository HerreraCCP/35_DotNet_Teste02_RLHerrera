using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClienteApi.Data.Mappings;
using ClienteApi.Models;
using System;
using System.Linq;

namespace ClienteApi.Data
{
    public sealed class ClienteDbContext : DbContext
    {
        private readonly ClienteDbContext _clienteDbContext;

        public ClienteDbContext(DbContextOptions<ClienteDbContext> options, ClienteDbContext clienteDbContext) : base(options) => _clienteDbContext = clienteDbContext;

        public DbSet<Acomodacao> Acomodacoes { get; set; }

        public DbSet<Arquivo> Arquivos { get; set; }

        public DbSet<DescricaoDespesa> DescricaoDespesas { get; set; }

        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArquivosMap());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in _clienteDbContext.ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedIn") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("CreatedIn").CurrentValue = DateTime.UtcNow;
                if (entry.State == EntityState.Modified) entry.Property("CreatedIn").IsModified = false;
            }

            return _clienteDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}