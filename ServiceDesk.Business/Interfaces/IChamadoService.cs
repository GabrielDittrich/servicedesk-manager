using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Business.Interfaces
{
    public interface IChamadoService
    {
        List<Chamado> Listar();

        Chamado? BuscarPorId(int id);

        void Criar(Chamado chamado);

        void AlterarStatus(int id, StatusChamado status, int? tecnicoResponsavelId);
    }
}