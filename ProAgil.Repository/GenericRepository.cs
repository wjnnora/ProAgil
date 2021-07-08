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
        public void Insert(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }

        /// <summary>
        /// Delete the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be deleted.</param>
        public void Delete(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Update the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be updated.</param>
        public void Update(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;            
        }

        /// <summary>
        /// Save changes.
        /// </summary>    
        /// <returns>
        /// Return a boolean that indicates whether changes was completed successfully. 
        /// </returns> 
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }        

        public async Task<bool> Exists(int id)
        {            
            return await _context.Set<T>().FindAsync(id) != null;
        }        
    }
}