using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Business.Interfaces;

namespace ServiceDesk.Business.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly AtendimentoRepository _atendimentoRepository;
        private readonly ChamadoRepository _chamadoRepository;

        public AtendimentoService(
            AtendimentoRepository atendimentoRepository,
            ChamadoRepository chamadoRepository)
        {
            _atendimentoRepository = atendimentoRepository;
            _chamadoRepository = chamadoRepository;
        }

        public List<Atendimento> ListarPorChamado(int chamadoId)
        {
            return _atendimentoRepository.ListarPorChamado(chamadoId);
        }

        public void Criar(Atendimento atendimento)
        {
            var chamado = _chamadoRepository.BuscarPorId(atendimento.ChamadoId);

            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            if (chamado.Status == StatusChamado.Cancelado)
            {
                throw new Exception("Não é possível adicionar atendimento em um chamado cancelado.");
            }

            if (chamado.Status == StatusChamado.Resolvido)
            {
                throw new Exception("Não é possível adicionar atendimento em um chamado resolvido.");
            }

            atendimento.Id = 0;
            atendimento.CriadoEm = DateTime.Now;

            chamado.Status = StatusChamado.EmAtendimento;
            chamado.TecnicoResponsavelId = atendimento.TecnicoId;
            chamado.AtualizadoEm = DateTime.Now;

            _atendimentoRepository.Adicionar(atendimento);
            _chamadoRepository.Atualizar(chamado);
        }
    }
}