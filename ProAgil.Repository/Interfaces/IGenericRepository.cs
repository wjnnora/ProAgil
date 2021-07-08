using System.Threading.Tasks;

namespace ProAgil.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
         void Insert(T Entity);
         void Update(T Entity);
         void Delete(T Entity);
         Task<bool> Exists(int id);
         Task<bool> SaveChangesAsync();
    }
}