using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Context;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository.Repositories
{
    public class EventoRepository : GenericRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ProAgilContext context) : base(context) { }

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <param name="includePalestrantes">Boolean that indicates whether Palestrantes shoud be included to the result.</param>
        /// <returns>An event list.</returns>
        public async Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            return await query.OrderByDescending(e => e.DataEvento).AsSplitQuery().ToArrayAsync();
        }

        /// <summary>
        /// Get an event by its id.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <param name="includePalestrantes">Boolean that indicates whether Palestrantes shoud be included to the result.</param>
        /// <returns>An event.</returns>
        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            return await query.AsSplitQuery().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Get an event by its name.
        /// </summary>
        /// <param name="tema">The event thema.</param>
        /// <param name="includePalestrantes">Boolean that indicates whether Palestrantes shoud be included to the result.</param>
        /// <returns>An event.</returns>
        public async Task<Evento> GetEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(lt => lt.Lotes)
                .Include(rs => rs.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            return await query.AsSplitQuery().FirstOrDefaultAsync(e => e.Tema.Contains(tema));
        }
    }
}