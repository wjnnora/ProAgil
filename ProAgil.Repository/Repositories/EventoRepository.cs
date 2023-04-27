using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using ProAgil.Domain;
using ProAgil.Repository.Context;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository.Repositories
{
    public class EventoRepository : GenericRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ProAgilContext context) : base(context) { }
        
        public async Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);

            return await query.OrderByDescending(e => e.DataEvento).AsSplitQuery().ToArrayAsync();
        }
        
        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);

            return await query.AsSplitQuery().FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<Evento> GetEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);

            return await query.AsSplitQuery().FirstOrDefaultAsync(e => e.Tema.Contains(tema));
        }
    }
}