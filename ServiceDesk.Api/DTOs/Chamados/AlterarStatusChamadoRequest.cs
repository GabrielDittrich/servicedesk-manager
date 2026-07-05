using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Api.DTOs.Chamados
{
    public class AlterarStatusChamadoRequest
    {
        public StatusChamado Status { get; set; }

        public int? TecnicoResponsavelId { get; set; }
    }
}