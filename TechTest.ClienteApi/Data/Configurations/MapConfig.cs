using ClienteApi.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Data.Configurations
{
    public static class MapConfig
    {
        public static void ConfigMapApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArquivosMap());
            builder.ApplyConfiguration(new AcomodacaoMap());
            builder.ApplyConfiguration(new DescricaoDespesaMap());
            builder.ApplyConfiguration(new DespesaMap());
        }
    }
}