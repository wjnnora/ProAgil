using System.Collections.Generic;

namespace ProAgil.Api.DTO
{
    public class PalestranteResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<EventoResponse> Eventos { get; set; }
        public List<RedeSocialResponse> RedesSociais { get; set; }        
    }
}