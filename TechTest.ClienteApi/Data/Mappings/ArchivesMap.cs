using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class ArchivesMap : IEntityTypeConfiguration<Archive>
    {
        public void Configure(EntityTypeBuilder<Archive> builder)
        {
            //TODO Mapear o [AspNetUsers] do Identity

            builder.ToTable("Archive");
            
            builder.HasKey(x => x.Id);
            
            builder
                .HasIndex(x => x.Id, "IX_File_Archive")
                .IsUnique();
            
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);
        }
    }
}