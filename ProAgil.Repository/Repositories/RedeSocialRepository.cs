using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Context;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository.Repositories
{
    public class RedesSociaisRepository : GenericRepository<RedeSocial>, IRedesSociaisRepository
    {
        public RedesSociaisRepository(ProAgilContext context) : base(context) { }

        public async Task<IEnumerable<RedeSocial>> GetAllRedesSociaisAsync()
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.OrderBy(x => x.Nome).ToArrayAsync();
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