using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository
{
    public class PalestranteContext : GenericRepository<Palestrante>, IPalestranteRepository
    {
        public PalestranteContext(EventoContext context): base(context)
        {
            
        }
        public async Task<IEnumerable<Palestrante>> GetAllPalestrantesAsync()
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(pe => pe.PalestranteEventos).ThenInclude(e => e.Evento)
                .Include(rs => rs.RedesSociais);

            return await query.OrderBy(n => n.Nome).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(pe => pe.PalestranteEventos).ThenInclude(e => e.Evento)
                .Include(rs => rs.RedesSociais);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Palestrante> GetPalestranteByNameAsync(string nome)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(pe => pe.PalestranteEventos).ThenInclude(e => e.Evento)
                .Include(rs => rs.RedesSociais);

            return await query.FirstOrDefaultAsync(x => x.Nome.Contains(nome));
        }        
    }
}