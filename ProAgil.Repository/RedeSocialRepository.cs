using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository
{
    public class RedesSociaisRepository: GenericRepository<RedeSocial>, IRedesSociaisRepository
    {
        public RedesSociaisRepository(EventoContext context): base(context)
        {
            
        }

        public async Task<IEnumerable<RedeSocial>> GetAllRedesSociaisAsync()
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.OrderBy(x => x.Nome).ToArrayAsync();
        }

        public async Task<RedeSocial> GetLastRedeSocialInserted()
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            RedeSocial redeSocial = await query.OrderByDescending(x => x.Id).LastOrDefaultAsync();

            if (redeSocial != null) 
            {
                return await this.GetRedeSocialByIdAsync(redeSocial.Id);
            }
            return redeSocial;
        }

        public async Task<RedeSocial> GetRedeSocialByIdAsync(int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;


            return await query.FirstOrDefaultAsync(rs => rs.Id == id);
        }

        public async Task<RedeSocial> GetRedeSocialByNomeAsync(string nome)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.FirstOrDefaultAsync(rs => rs.Nome.ToLower().Contains(nome.ToLower()));
        }
    }
}