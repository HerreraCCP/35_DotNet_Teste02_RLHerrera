using ClienteApi.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Data.Configurations
{
    public static class MapConfiguration
    {
        public static void ResolveMapConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TypeOfLocationMap());
            builder.ApplyConfiguration(new TypeOfAccommodationMap());
            builder.ApplyConfiguration(new ExpenseDescriptionMap());
            builder.ApplyConfiguration(new ExpenseMap());
            builder.ApplyConfiguration(new ArchivesMap());
        }
    }
}