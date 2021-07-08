using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface ILoteRepository: IGenericRepository<Lote>
    {
        Task<IEnumerable<Lote>> GetAllLotesAsync();
        Task<Lote> GetLoteByIdAsync(int id);
    }
}