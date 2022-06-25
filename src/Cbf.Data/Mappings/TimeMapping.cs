using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbf.Data.Mappings
{
    public class TimeMapping : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(t => t.Localidade)
                   .IsRequired()
                   .HasColumnType("varchar(255)");

            builder.HasMany(t => t.Jogadores)
                   .WithOne(t => t.Time)
                   .HasForeignKey(t => t.TimeId);

            builder.HasMany(t => t.Transferencias)
                   .WithOne(t => t.Time)
                   .HasForeignKey(t => t.TimeOrigemId);

            builder.ToTable("Times");
        }
    }
}
