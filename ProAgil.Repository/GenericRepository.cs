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
        /// <returns>
        /// Return the entity inserted.
        /// </returns>
        public async Task<T> Insert(T Entity)
        {
            _context.Set<T>().Add(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        /// <summary>
        /// Delete the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be deleted.</param>
        /// <returns>
        /// Return a boolean that indicates whether the entity was deleted.
        /// </returns>
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
        /// <returns>
        /// Return the entity updated.
        /// </returns>
        public async Task<T> Update(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Entity;
        }     
    }
}