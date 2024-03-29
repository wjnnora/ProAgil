using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProAgil.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> Insert(T Entity);
        Task<T> Update(T Entity);
        Task<bool> Delete(T Entity);
        Task<bool> DeleteRange(List<T> objs);
    }
}