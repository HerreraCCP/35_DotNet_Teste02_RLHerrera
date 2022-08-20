using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsuariosApi.Data;

namespace UsuariosApi.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UsuarioConnection");
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
            services
                .AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt => opt.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}