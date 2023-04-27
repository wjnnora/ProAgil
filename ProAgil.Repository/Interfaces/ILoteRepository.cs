using System.Threading.Tasks;
using System.Collections.Generic;

using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface ILoteRepository: IGenericRepository<Lote>
    {
        Task<IEnumerable<Lote>> GetAllLotesAsync();
        Task<Lote> GetLoteByIdAsync(int id);        
    }
}