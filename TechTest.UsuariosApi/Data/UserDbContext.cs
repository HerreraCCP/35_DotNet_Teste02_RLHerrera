using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UsuariosApi.Data
{
    public sealed class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in builder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            builder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            base.OnModelCreating(builder);

            //User Admin
            IdentityUser<int> admin = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "herrera.ccp@gmail.com",
                NormalizedEmail = "HERRERA.CCP@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999999
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();
            admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("admininfo:password"));

            builder.Entity<IdentityUser<int>>().HasData(admin);
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 99999999, Name = "admin", NormalizedName = "ADMIN" });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 00000010, Name = "regular", NormalizedName = "REGULAR" });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { RoleId = 99999999, UserId = 99999999 });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.UtcNow; //CreateUsuarioDto
                if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}