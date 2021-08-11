using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Api.DTO
{
    public class EventoDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Local { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]        
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo {0} deve ter entre 2 e 100 caracteres.")]        
        public string Tema { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(2, 999, ErrorMessage = "O campo {0} deve ser entre 2 e 999.")]
        public int QtdPessoas { get; set; }

        public string ImagePath { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}