using ServiceDesk.Business.Exceptions;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Business.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly AtendimentoRepository _atendimentoRepository;
        private readonly ChamadoRepository _chamadoRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public AtendimentoService(
            AtendimentoRepository atendimentoRepository,
            ChamadoRepository chamadoRepository,
            UsuarioRepository usuarioRepository)
        {
            _atendimentoRepository = atendimentoRepository;
            _chamadoRepository = chamadoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<Atendimento> ListarPorChamado(int chamadoId)
        {
            if (chamadoId <= 0)
            {
                throw new RegraDeNegocioException("O ID do chamado é inválido.");
            }

            var chamado = _chamadoRepository.BuscarPorId(chamadoId);

            if (chamado == null)
            {
                throw new RecursoNaoEncontradoException("Chamado não encontrado.");
            }

            return _atendimentoRepository.ListarPorChamado(chamadoId);
        }

        public void Criar(Atendimento atendimento)
        {
            if (atendimento == null)
            {
                throw new RegraDeNegocioException("Os dados do atendimento são obrigatórios.");
            }

            if (atendimento.ChamadoId <= 0)
            {
                throw new RegraDeNegocioException("O chamado do atendimento é obrigatório.");
            }

            if (atendimento.TecnicoId <= 0)
            {
                throw new RegraDeNegocioException("O técnico do atendimento é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(atendimento.Descricao))
            {
                throw new RegraDeNegocioException("A descrição do atendimento é obrigatória.");
            }

            var chamado = _chamadoRepository.BuscarPorId(atendimento.ChamadoId);

            if (chamado == null)
            {
                throw new RecursoNaoEncontradoException("Chamado não encontrado.");
            }

            if (chamado.Status == StatusChamado.Cancelado)
            {
                throw new RegraDeNegocioException("Não é possível adicionar atendimento em um chamado cancelado.");
            }

            if (chamado.Status == StatusChamado.Resolvido)
            {
                throw new RegraDeNegocioException("Não é possível adicionar atendimento em um chamado resolvido.");
            }

            var tecnico = _usuarioRepository.BuscarPorId(atendimento.TecnicoId);

            if (tecnico == null)
            {
                throw new RecursoNaoEncontradoException("Técnico não encontrado.");
            }

            if (!tecnico.Ativo)
            {
                throw new RegraDeNegocioException("Não é possível adicionar atendimento com um técnico inativo.");
            }

            atendimento.Id = 0;
            atendimento.Descricao = atendimento.Descricao.Trim();
            atendimento.CriadoEm = DateTime.Now;

            chamado.Status = StatusChamado.EmAtendimento;
            chamado.TecnicoResponsavelId = atendimento.TecnicoId;
            chamado.AtualizadoEm = DateTime.Now;

            _atendimentoRepository.Adicionar(atendimento);
            _chamadoRepository.Atualizar(chamado);
        }
    }
}