using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository
{
    public class PalestranteRepository : GenericRepository<Palestrante>, IPalestranteRepository
    {
        public PalestranteRepository(ProAgilContext context): base(context) { }
        
        public async Task<IEnumerable<Palestrante>> GetAllPalestrantesAsync()
        {
            IQueryable<Palestrante> query = _context.Palestrantes                
                .Include(rs => rs.RedesSociais);

            return await query.AsSplitQuery().ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id)
        {
            IQueryable<Palestrante> query = _context.Palestrantes                
                .Include(rs => rs.RedesSociais);

            return await query.AsSplitQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Palestrante> GetPalestranteByNomeAsync(string nome)
        {
            IQueryable<Palestrante> query = _context.Palestrantes                
                .Include(rs => rs.RedesSociais);

            return await query.AsSplitQuery().FirstOrDefaultAsync(x => x.Nome.ToLower().Contains(nome.ToLower()));
        }        
    }
}