using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbf.Data.Mappings
{
    public class JogadorMapping : IEntityTypeConfiguration<Jogador>
    {
        public void Configure(EntityTypeBuilder<Jogador> builder)
        {
            builder.HasKey(j => j.Id);

            builder.Property(j => j.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(j => j.DataNascimento)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(j => j.Pais)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(j => j.Salario)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(j => j.Posicao)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.HasMany(t => t.Transferencias)
                   .WithOne(t => t.Jogador)
                   .HasForeignKey(t => t.JogadorId);

            builder.ToTable("Jogadores");
        }
    }
}
