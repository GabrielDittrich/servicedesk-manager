using ServiceDesk.Data.Context;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Repositories
{
    public class CategoriaRepository
    {
        private readonly ServiceDeskDbContext _context;

        public CategoriaRepository(ServiceDeskDbContext context)
        {
            _context = context;
        }

        public List<Categoria> Listar()
        {
            return _context.Categorias.ToList();
        }

        public List<Categoria> ListarAtivas()
        {
            return _context.Categorias
                .Where(categoria => categoria.Ativa)
                .ToList();
        }

        public Categoria? BuscarPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public Categoria? BuscarPorNome(string nome)
        {
            return _context.Categorias
                .FirstOrDefault(categoria => categoria.Nome == nome);
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void Remover(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }
    }
}