using ServiceDesk.Business.Exceptions;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(
            UsuarioRepository usuarioRepository,
            IPasswordHasher<Usuario> passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public List<Usuario> Listar()
        {
            return _usuarioRepository.Listar();
        }

        public Usuario? BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new RegraDeNegocioException("O ID do usuário é inválido.");
            }

            return _usuarioRepository.BuscarPorId(id);
        }

        public void Criar(Usuario usuario, string senha)
        {
            if (usuario == null)
            {
                throw new RegraDeNegocioException("Os dados do usuário são obrigatórios.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                throw new RegraDeNegocioException("O nome do usuário é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                throw new RegraDeNegocioException("O e-mail do usuário é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new RegraDeNegocioException("A senha do usuário é obrigatória.");
            }

            if (senha.Length < 6)
            {
                throw new RegraDeNegocioException("A senha deve ter pelo menos 6 caracteres.");
            }

            var emailJaExiste = _usuarioRepository
                .Listar()
                .Any(u => u.Email.ToLower() == usuario.Email.ToLower());

            if (emailJaExiste)
            {
                throw new RegraDeNegocioException("Já existe um usuário cadastrado com este e-mail.");
            }

            usuario.Id = 0;
            usuario.Nome = usuario.Nome.Trim();
            usuario.Email = usuario.Email.Trim().ToLower();
            usuario.Ativo = true;
            usuario.CriadoEm = DateTime.Now;

            if (usuario.Perfil == 0)
            {
                usuario.Perfil = PerfilUsuario.Solicitante;
            }

            // Temporário para estudo. Depois vamos trocar por hash real.
            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, senha);

            _usuarioRepository.Adicionar(usuario);
        }

        public void Remover(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new RecursoNaoEncontradoException("Usuário não encontrado.");
            }

            _usuarioRepository.Remover(usuario);
        }
    }
}