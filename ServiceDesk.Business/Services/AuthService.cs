using Microsoft.AspNetCore.Identity;
using ServiceDesk.Business.Exceptions;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AuthService(
            UsuarioRepository usuarioRepository,
            IPasswordHasher<Usuario> passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public Usuario Autenticar(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new RegraDeNegocioException("O e-mail é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new RegraDeNegocioException("A senha é obrigatória.");
            }

            var usuario = _usuarioRepository.BuscarPorEmail(email.Trim().ToLower());

            if (usuario == null)
            {
                throw new RegraDeNegocioException("E-mail ou senha inválidos.");
            }

            if (!usuario.Ativo)
            {
                throw new RegraDeNegocioException("Usuário inativo.");
            }

            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.SenhaHash,
                senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                throw new RegraDeNegocioException("E-mail ou senha inválidos.");
            }

            return usuario;
        }
    }
}