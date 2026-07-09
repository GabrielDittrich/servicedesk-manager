using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Api.DTOs.Chamados
{
    public class ChamadoResponse
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public StatusChamado Status { get; set; }

        public PrioridadeChamado Prioridade { get; set; }

        public int CategoriaId { get; set; }

        public int SolicitanteId { get; set; }

        public int? TecnicoResponsavelId { get; set; }

        public DateTime CriadoEm { get; set; }

        public DateTime? AtualizadoEm { get; set; }

        public DateTime? FinalizadoEm { get; set; }
    }
}