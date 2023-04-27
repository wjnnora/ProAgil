using System.Threading.Tasks;
using System.Collections.Generic;

using ProAgil.Domain;

namespace ProAgil.Repository.Interfaces
{
    public interface IEventoRepository: IGenericRepository<Evento>
    {
         Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false);
         Task<Evento> GetEventosByTemaAsync(string tema, bool includePalestrantes = false);
         Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false);         
    }
}