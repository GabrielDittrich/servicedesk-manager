using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Business.Interfaces;

namespace ServiceDesk.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<Usuario> Listar()
        {
            return _usuarioRepository.Listar();
        }

        public Usuario? BuscarPorId(int id)
        {
            return _usuarioRepository.BuscarPorId(id);
        }

        public void Criar(Usuario usuario, string senha)
        {
            usuario.Id = 0;
            usuario.Ativo = true;
            usuario.CriadoEm = DateTime.Now;

            if (usuario.Perfil == 0)
            {
                usuario.Perfil = PerfilUsuario.Solicitante;
            }

            // Temporário para estudo.
            // Depois o correto é trocar por hash de senha.
            usuario.SenhaHash = senha;

            _usuarioRepository.Adicionar(usuario);
        }

        public void Remover(Usuario usuario)
        {
            _usuarioRepository.Remover(usuario);
        }
    }
}