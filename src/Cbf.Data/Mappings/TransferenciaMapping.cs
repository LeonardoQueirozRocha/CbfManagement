using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbf.Data.Mappings
{
    public class TransferenciaMapping : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TimeOrigemId)
                   .IsRequired();

            builder.Property(t => t.TimeDestinoId)
                   .IsRequired();

            builder.Property(t => t.JogadorId)
                   .IsRequired();

            builder.Property(t => t.Valor)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.ToTable("Transferencias");
        }
    }
}
