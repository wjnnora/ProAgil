using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using ProAgil.Repository.Context;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ProAgilContext _context;

        public GenericRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public async Task<T> Insert(T Entity)
        {
            _context.Set<T>().Add(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
        
        public async Task<bool> Delete(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteRange(List<T> Entity)
        {
            _context.Set<List<T>>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;

        }
        
        public async Task<T> Update(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Entity;
        }
    }
}