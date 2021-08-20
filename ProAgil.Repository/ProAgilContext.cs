using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Repository
{
    public class ProAgilContext: IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {    
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base(options) {}        

        public DbSet<Evento> Eventos { get; set; }    
        public DbSet<Palestrante> Palestrantes { get; set; }                
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => {

                userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                userRole.HasOne(r => r.Role)
                    .WithMany(ur => ur.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(u => u.User)
                    .WithMany(ur => ur.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(pe => new { pe.EventoId, pe.PalestranteId });
        }        
    }
}