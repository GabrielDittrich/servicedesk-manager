using ServiceDesk.Data.Context;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Data.Repositories
{
    public class AtendimentoRepository
    {
        private readonly ServiceDeskDbContext _context;

        public AtendimentoRepository(ServiceDeskDbContext context)
        {
            _context = context;
        }

        public List<Atendimento> ListarPorChamado(int chamadoId)
        {
            return _context.Atendimentos
                .Where(a => a.ChamadoId == chamadoId)
                .ToList();
        }

        public void Adicionar(Atendimento atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
        }
    }
}