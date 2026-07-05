using ServiceDesk.Data.Repositories;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Business.Interfaces;

namespace ServiceDesk.Business.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<Categoria> Listar()
        {
            return _categoriaRepository.Listar();
        }

        public Categoria? BuscarPorId(int id)
        {
            return _categoriaRepository.BuscarPorId(id);
        }

        public void Criar(Categoria categoria)
        {
            categoria.Id = 0;
            categoria.Ativa = true;
            categoria.CriadoEm = DateTime.Now;

            _categoriaRepository.Adicionar(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _categoriaRepository.Atualizar(categoria);
        }

        public void Remover(Categoria categoria)
        {
            _categoriaRepository.Remover(categoria);
        }
    }
}