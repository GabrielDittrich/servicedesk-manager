using ServiceDesk.Business.Exceptions;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Business.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly ChamadoRepository _chamadoRepository;
        private readonly CategoriaRepository _categoriaRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public ChamadoService(
            ChamadoRepository chamadoRepository,
            CategoriaRepository categoriaRepository,
            UsuarioRepository usuarioRepository)
        {
            _chamadoRepository = chamadoRepository;
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<Chamado> Listar()
        {
            return _chamadoRepository.Listar();
        }

        public Chamado? BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new RegraDeNegocioException("O ID do chamado é inválido.");
            }

            return _chamadoRepository.BuscarPorId(id);
        }

        public void Criar(Chamado chamado)
        {
            if (chamado == null)
            {
                throw new RegraDeNegocioException("Os dados do chamado são obrigatórios.");
            }

            if (string.IsNullOrWhiteSpace(chamado.Titulo))
            {
                throw new RegraDeNegocioException("O título do chamado é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(chamado.Descricao))
            {
                throw new RegraDeNegocioException("A descrição do chamado é obrigatória.");
            }

            if (chamado.CategoriaId <= 0)
            {
                throw new RegraDeNegocioException("A categoria do chamado é obrigatória.");
            }

            if (chamado.SolicitanteId <= 0)
            {
                throw new RegraDeNegocioException("O solicitante do chamado é obrigatório.");
            }

            var categoria = _categoriaRepository.BuscarPorId(chamado.CategoriaId);

            if (categoria == null)
            {
                throw new RecursoNaoEncontradoException("Categoria não encontrada.");
            }

            if (!categoria.Ativa)
            {
                throw new RegraDeNegocioException("Não é possível criar chamado para uma categoria inativa.");
            }

            var solicitante = _usuarioRepository.BuscarPorId(chamado.SolicitanteId);

            if (solicitante == null)
            {
                throw new RecursoNaoEncontradoException("Solicitante não encontrado.");
            }

            if (!solicitante.Ativo)
            {
                throw new RegraDeNegocioException("Não é possível criar chamado para um solicitante inativo.");
            }

            chamado.Id = 0;
            chamado.Titulo = chamado.Titulo.Trim();
            chamado.Descricao = chamado.Descricao.Trim();
            chamado.Status = StatusChamado.Aberto;
            chamado.CriadoEm = DateTime.Now;
            chamado.AtualizadoEm = null;
            chamado.FinalizadoEm = null;

            _chamadoRepository.Adicionar(chamado);
        }

        public void AlterarStatus(int id, StatusChamado status, int? tecnicoResponsavelId)
        {
            if (id <= 0)
            {
                throw new RegraDeNegocioException("O ID do chamado é inválido.");
            }

            if (!Enum.IsDefined(typeof(StatusChamado), status))
            {
                throw new RegraDeNegocioException("Status do chamado inválido.");
            }

            var chamado = _chamadoRepository.BuscarPorId(id);

            if (chamado == null)
            {
                throw new RecursoNaoEncontradoException("Chamado não encontrado.");
            }

            if (chamado.Status == StatusChamado.Cancelado)
            {
                throw new RegraDeNegocioException("Não é possível alterar um chamado cancelado.");
            }

            if (chamado.Status == StatusChamado.Resolvido)
            {
                throw new RegraDeNegocioException("Não é possível alterar um chamado resolvido.");
            }

            if (status == StatusChamado.EmAtendimento && tecnicoResponsavelId == null)
            {
                throw new RegraDeNegocioException("Para colocar o chamado em atendimento, informe o técnico responsável.");
            }

            if (tecnicoResponsavelId.HasValue)
            {
                var tecnico = _usuarioRepository.BuscarPorId(tecnicoResponsavelId.Value);

                if (tecnico == null)
                {
                    throw new RecursoNaoEncontradoException("Técnico responsável não encontrado.");
                }

                if (!tecnico.Ativo)
                {
                    throw new RegraDeNegocioException("O técnico responsável está inativo.");
                }
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