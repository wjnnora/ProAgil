using System.ComponentModel.DataAnnotations;

namespace ProAgil.Api.DTO
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string URL { get; set; }
    }
}