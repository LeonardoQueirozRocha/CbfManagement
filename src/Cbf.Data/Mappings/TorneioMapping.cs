using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbf.Data.Mappings
{
    public class TorneioMapping : IEntityTypeConfiguration<Torneio>
    {
        public void Configure(EntityTypeBuilder<Torneio> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.HasMany(p => p.Partidas)
                   .WithOne(t => t.Torneio)
                   .HasForeignKey(t => t.TorneioId);

            builder.ToTable("Torneios");
        }
    }
}
