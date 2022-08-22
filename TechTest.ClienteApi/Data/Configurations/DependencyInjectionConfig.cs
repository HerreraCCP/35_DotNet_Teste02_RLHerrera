using Microsoft.Extensions.DependencyInjection;

namespace ClienteApi.Data.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}