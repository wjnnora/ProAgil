using System.ComponentModel.DataAnnotations;

namespace ProAgil.Api.DTO
{
    public class LoteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal Preco { get; set; }
        public string DataInicio{ get; set; }
        public string DataFim { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(2, 100)]
        public int Quantidade { get; set; }      
        public int EventoId { get; set; }          
    }
}