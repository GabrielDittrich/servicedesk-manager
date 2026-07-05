using Microsoft.EntityFrameworkCore;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Context
{
    public class ServiceDeskDbContext : DbContext
    {
        public ServiceDeskDbContext(DbContextOptions<ServiceDeskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;

        public DbSet<Categoria> Categorias { get; set; } = null!;

        public DbSet<Chamado> Chamados { get; set; } = null!;

        public DbSet<Atendimento> Atendimentos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDeskDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}