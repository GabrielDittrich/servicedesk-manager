using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Maps
{
    public class ChamadoMap : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamados");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.Prioridade)
                .IsRequired();

            builder.Property(c => c.CategoriaId)
                .IsRequired();

            builder.Property(c => c.SolicitanteId)
                .IsRequired();

            builder.Property(c => c.TecnicoResponsavelId)
                .IsRequired(false);

            builder.Property(c => c.CriadoEm)
                .IsRequired();

            builder.Property(c => c.AtualizadoEm)
                .IsRequired(false);

            builder.Property(c => c.FinalizadoEm)
                .IsRequired(false);
        }
    }
}