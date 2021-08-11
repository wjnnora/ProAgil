using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Api.DTO
{
    public class PalestranteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<EventoDTO> Eventos { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }        
    }
}