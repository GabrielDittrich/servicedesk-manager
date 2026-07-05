using ServiceDesk.Domain.Entities;


namespace ServiceDesk.Business.Interfaces
{
    public interface IAtendimentoService
    {
        List<Atendimento> ListarPorChamado(int chamadoId);

        void Criar(Atendimento atendimento);
    }
}