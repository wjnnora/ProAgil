using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        protected readonly EventoContext _context;
        public GenericRepository(EventoContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Save the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be saved.</param>
        /// <return>
        /// Return a boolean that indicating wheter the insertion was completed successfully.
        /// </return>
        public async Task<bool> Insert(T Entity)
        {
            _context.Set<T>().Add(Entity);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be deleted.</param>
        /// <return>
        /// Return a boolean that indicating wheter the deletion was completed successfully.
        /// </return>
        public async Task<bool> Delete(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Update the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be updated.</param>
        public async Task<bool> Update(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }            

        public async Task<bool> Exists(int id)
        {            
            return await _context.Set<T>().FindAsync(id) != null;
        }        
    }
}