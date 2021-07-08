namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int EventoId { get; set; }
        public Evento Evento { get; }
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; }
    }
}