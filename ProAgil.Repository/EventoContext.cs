using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class EventoContext: DbContext
    {    
        public EventoContext(DbContextOptions<EventoContext> options) : base(options) {}        

        public DbSet<Evento> Eventos { get; set; }    
        public DbSet<Palestrante> Palestrantes { get; set; }                
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(pe => new { pe.EventoId, pe.PalestranteId });
        }        
    }
}