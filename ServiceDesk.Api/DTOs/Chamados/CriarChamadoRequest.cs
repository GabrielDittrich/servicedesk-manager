using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Api.DTOs.Chamados
{
    public class CriarChamadoRequest
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public int CategoriaId { get; set; }

        public int SolicitanteId { get; set; }

        public PrioridadeChamado Prioridade { get; set; } = PrioridadeChamado.Media;
    }
}