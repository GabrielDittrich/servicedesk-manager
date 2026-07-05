using ServiceDesk.Data.Context;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Repositories
{
    public class ChamadoRepository
    {
        private readonly ServiceDeskDbContext _context;

        public ChamadoRepository(ServiceDeskDbContext context)
        {
            _context = context;
        }

        public List<Chamado> Listar()
        {
            return _context.Chamados.ToList();
        }

        public Chamado? BuscarPorId(int id)
        {
            return _context.Chamados.Find(id);
        }

        public void Adicionar(Chamado chamado)
        {
            _context.Chamados.Add(chamado);
            _context.SaveChanges();
        }

        public void Atualizar(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            _context.SaveChanges();
        }

        public void Remover(Chamado chamado)
        {
            _context.Chamados.Remove(chamado);
            _context.SaveChanges();
        }
    }
}