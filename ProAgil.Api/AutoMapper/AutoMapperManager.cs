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
            CreateMap<Evento, EventoDTO>()
                .ForMember(from => from.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                }).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(from => from.Eventos, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList());
                }).ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}