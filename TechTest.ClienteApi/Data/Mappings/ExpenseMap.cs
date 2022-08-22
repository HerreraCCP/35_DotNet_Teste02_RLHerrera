using System;
using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class ExpenseMap : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expense");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_BarCode_Expense")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.BarCode)
                .IsRequired()
                .HasColumnName("BarCode")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(155);

            builder.Property(x => x.CreatedIn)
                .IsRequired()
                .HasColumnName("CreatedIn")
                .HasColumnType("datetime")
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.ExpireIn)
                .IsRequired()
                .HasColumnName("ExpireIn")
                .HasColumnType("datetime2");

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("decimal");

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);

            builder
                .HasOne(x => x.ExpenseDescription)
                .WithMany(x => x.Expenses)
                .HasConstraintName("FK_Expenses_ExpenseDescription")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.TypeOfLocation)
                .WithMany(x => x.Expenses)
                .HasConstraintName("FK_Expenses_TypeOfLocation")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.TypeOfAccommodation)
                .WithMany(x => x.Expenses)
                .HasConstraintName("FK_Expenses_TypeOfAccommodation")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}