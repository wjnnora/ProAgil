using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface IRedesSociaisRepository: IGenericRepository<RedeSocial>
    {
         Task<RedeSocial> GetRedeSocialByIdAsync(int id);
         Task<RedeSocial> GetRedeSocialByNomeAsync(string nome);
         Task<IEnumerable<RedeSocial>> GetAllRedesSociaisAsync();
    }
}