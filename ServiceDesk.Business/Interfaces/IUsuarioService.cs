using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Business.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> Listar();

        Usuario? BuscarPorId(int id);

        void Criar(Usuario usuario, string senha);

        void Remover(Usuario usuario);
    }
}