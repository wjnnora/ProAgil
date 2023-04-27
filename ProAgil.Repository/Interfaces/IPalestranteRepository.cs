using System.Threading.Tasks;
using System.Collections.Generic;

using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface IPalestranteRepository: IGenericRepository<Palestrante>
    {
        Task<Palestrante> GetPalestranteByIdAsync(int id);         
        Task<Palestrante> GetPalestranteByNomeAsync(string nome);
        Task<IEnumerable<Palestrante>> GetAllPalestrantesAsync();        
    }
}