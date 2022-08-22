using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Data.Configurations
{
    public sealed class DbSetConfig
    {
        public DbSet<Archive> Archives { get; set; }
        
        public DbSet<Expense> Expenses { get; set; }
        
        public DbSet<ExpenseDescription> ExpenseDescriptions { get; set; }
        
        public DbSet<TypeOfAccommodation> TAccommodations { get; set; }
        
        public DbSet<TypeOfLocation> TLocations { get; set; }
    }
}