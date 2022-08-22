using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClienteApi.Data.Configurations;

namespace ClienteApi.Data
{
    using static Configurations.MapConfiguration;

    public sealed class ClienteDbContext : DbContext
    {
        private DbSetConfig SetConfig { get; }

        public ClienteDbContext(DbContextOptions<ClienteDbContext> opt, DbSetConfig dbSetConfig) : base(opt)
        {
            SetConfig = dbSetConfig;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties()
                             .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            builder.ApplyConfigurationsFromAssembly(typeof(ClienteDbContext).Assembly);

            foreach (var relationship in builder.Model
                         .GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            ResolveMapConfiguration(builder);

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries()
                         .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("CreatedIn").CurrentValue = DateTime.UtcNow;
                if (entry.State == EntityState.Modified) entry.Property("CreatedIn").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        
    }
}