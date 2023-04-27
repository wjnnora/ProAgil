using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using ProAgil.Domain;
using ProAgil.Repository.Context;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository.Repositories
{
    public class LoteRepository : GenericRepository<Lote>, ILoteRepository
    {
        public LoteRepository(ProAgilContext context) : base(context) { }

        public async Task<IEnumerable<Lote>> GetAllLotesAsync()
        {
            IQueryable<Lote> query = _context.Lotes;

            return await query.OrderBy(x => x.EventoId).ThenBy(x => x.DataInicio).AsSplitQuery().ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdAsync(int id)
        {
            IQueryable<Lote> query = _context.Lotes;

            return await query.AsSplitQuery().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}