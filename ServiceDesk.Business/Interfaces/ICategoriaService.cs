using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Business.Interfaces
{
    public interface ICategoriaService
    {
        List<Categoria> Listar();

        Categoria? BuscarPorId(int id);

        void Criar(Categoria categoria);

        void Atualizar(Categoria categoria);

        void Remover(Categoria categoria);
    }
}