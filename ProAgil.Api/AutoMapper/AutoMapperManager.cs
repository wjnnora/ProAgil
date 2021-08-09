using System.Linq;
using AutoMapper;
using ProAgil.Api.DTO;
using ProAgil.Domain;

namespace ProAgil.Api.AutoMapper
{
    public class AutoMapperManager: Profile
    {
        public AutoMapperManager()
        {
            CreateMap<Evento, EventoResponse>()
                .ForMember(from => from.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                });
            CreateMap<Lote, LoteResponse>();
            CreateMap<Palestrante, PalestranteResponse>()
                .ForMember(from => from.Eventos, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList());
                });
            CreateMap<RedeSocial, RedeSocialResponse>();
        }
    }
}