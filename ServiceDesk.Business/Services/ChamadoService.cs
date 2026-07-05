using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Business.Interfaces;

namespace ServiceDesk.Business.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly ChamadoRepository _chamadoRepository;

        public ChamadoService(ChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        public List<Chamado> Listar()
        {
            return _chamadoRepository.Listar();
        }

        public Chamado? BuscarPorId(int id)
        {
            return _chamadoRepository.BuscarPorId(id);
        }

        public void Criar(Chamado chamado)
        {
            chamado.Id = 0;
            chamado.Status = StatusChamado.Aberto;
            chamado.CriadoEm = DateTime.Now;
            chamado.AtualizadoEm = null;
            chamado.FinalizadoEm = null;

            _chamadoRepository.Adicionar(chamado);
        }

        public void AlterarStatus(int id, StatusChamado status, int? tecnicoResponsavelId)
        {
            var chamado = _chamadoRepository.BuscarPorId(id);

            if (chamado == null)
            {
                return;
            }

            chamado.Status = status;
            chamado.TecnicoResponsavelId = tecnicoResponsavelId;
            chamado.AtualizadoEm = DateTime.Now;

            if (status == StatusChamado.Resolvido || status == StatusChamado.Cancelado)
            {
                chamado.FinalizadoEm = DateTime.Now;
            }

            _chamadoRepository.Atualizar(chamado);
        }
    }
}