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

        public ClienteDbContext(DbContextOptions<ClienteDbContext> options, ClienteDbContext clienteDbContext) : base(options)
        {
            _clienteDbContext = clienteDbContext;
        }

        public DbSet<Archive> Archives { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseDescription> ExpenseDescriptions { get; set; }

        public DbSet<TypeOfAccommodation> TypeOfAccommodations { get; set; }

        public DbSet<TypeOfLocation> TypeOfLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArchivesMap());
            modelBuilder.ApplyConfiguration(new ExpenseDescriptionMap());
            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new TypeOfAccommodationMap());
            modelBuilder.ApplyConfiguration(new TypeOfLocationMap());
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