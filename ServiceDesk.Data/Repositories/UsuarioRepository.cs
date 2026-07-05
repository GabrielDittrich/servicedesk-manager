using ServiceDesk.Data.Context;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly ServiceDeskDbContext _context;

        public UsuarioRepository(ServiceDeskDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario? BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Remover(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}