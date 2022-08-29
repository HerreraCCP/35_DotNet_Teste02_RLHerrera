using ClienteApi.Authorization;
using ClienteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ClienteApi.Data.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ArquivoService, ArquivoService>();
            services.AddScoped<DespesaService, DespesaService>();
            services.AddScoped<AcomodacaoService, AcomodacaoService>();
            services.AddScoped<DescricaoDespesaService, DescricaoDespesaService>();
            services.AddSingleton<IAuthorizationHandler, MyPolicyRequirementHandler>();
        }
    }
}