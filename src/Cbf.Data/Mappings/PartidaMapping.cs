using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbf.Data.Mappings
{
    public class PartidaMapping : IEntityTypeConfiguration<Partida>
    {
        public void Configure(EntityTypeBuilder<Partida> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataPartida)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(p => p.Resultado)
                   .IsRequired()
                   .HasColumnType("varchar(5)");

            builder.ToTable("Partidas");
        }
    }
}
