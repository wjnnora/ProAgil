using System.Threading.Tasks;

namespace ProAgil.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
         Task<bool> Insert(T Entity);
         Task<bool> Update(T Entity);
         Task<bool> Delete(T Entity);
         Task<bool> Exists(int id);         
    }
}