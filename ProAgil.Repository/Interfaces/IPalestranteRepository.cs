using ProAgil.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProAgil.Repository.Interfaces
{
    public interface IPalestranteRepository: IGenericRepository<Palestrante>
    {
        Task<Palestrante> GetPalestranteByIdAsync(int id);         
        Task<Palestrante> GetPalestranteByNomeAsync(string nome);
        Task<IEnumerable<Palestrante>> GetAllPalestrantesAsync();
    }
}