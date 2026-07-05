using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Domain.Entities
{
    public class Chamado
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public StatusChamado Status { get; set; } = StatusChamado.Aberto;

        public PrioridadeChamado Prioridade { get; set; } = PrioridadeChamado.Media;

        public int CategoriaId { get; set; }

        public int SolicitanteId { get; set; }

        public int? TecnicoResponsavelId { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public DateTime? AtualizadoEm { get; set; }

        public DateTime? FinalizadoEm { get; set; }
    }

}