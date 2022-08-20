using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UsuariosApi.Services;

namespace UsuariosApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<EmailService, EmailService>();
            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<LogoutService, LogoutService>();

            services.AddSingleton(typeof(ILogger), services.BuildServiceProvider().GetService<ILogger<EmailService>>());
            
            return services;
        }
    }
}