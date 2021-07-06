using System.Threading.Tasks;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        protected readonly EventoContext _context;
        public GenericRepository(EventoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be saved.</param>
        public void Insert(T Entity)
        {
            _context.Add(Entity);
        }

        /// <summary>
        /// Delete the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be deleted.</param>
        public virtual void Delete(T Entity)
        {
            _context.Remove(Entity);
        }

        /// <summary>
        /// Update the entity.
        /// </summary>        
        /// <param name="Entity">The entity that will be updated.</param>
        public void Update(T Entity)
        {
            _context.Update(Entity);
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

    }
}