using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface IRedesSociaisRepository
    {
         Task<RedeSocial> GetRedeSocialByIdAsync();
         Task<RedeSocial> GetRedeSocialByNameAsync();
         Task<IEnumerable<RedeSocial>> GetAllRedesSociaisAsync();
    }
}