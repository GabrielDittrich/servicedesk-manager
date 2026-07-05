using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Maps
{
    public class AtendimentoMap : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.ToTable("Atendimentos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ChamadoId)
                .IsRequired();

            builder.Property(a => a.TecnicoId)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(a => a.CriadoEm)
                .IsRequired();
        }
    }
}