using AutoMapper;
using System.Linq;

using ProAgil.Domain;
using ProAgil.Api.DTO;
using ProAgil.Api.DTO.User;
using ProAgil.Domain.Identity;

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
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}