using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGuanajuato.Data.Modelos;
using SistemaGuanajuato.Data.Modelos.Dto;

namespace SistemaGuanajuato.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegistroDto> Registros { get; set; }
        public DbSet<UsuarioInfo> UsuarioInfo { get; set; }
        public DbSet<EstadoRegistro> Estados { get; set; }
        public DbSet<UsuarioAD> UsuarioAD {  get; set; }       
        public DbSet<RespuestaDto> respuestaDto { get; set; }
        public DbSet<LoginDto> LoginDto {  get; set; }
        public DbSet<ValidacionDto> validacionDto {  get; set; }
        public DbSet<UsuarioEstatus> usuarioEstatus {  get; set; }
        public DbSet<UsuarioInfoDto> usuarioInfoDto { get; set; }
        public DbSet<ControlUsuarioDto> controlUsuarioDto {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<LoginDto>().HasNoKey();
            modelBuilder.Entity<RespuestaDto>().HasNoKey();
            modelBuilder.Entity<RegistroDto>().HasNoKey();
            modelBuilder.Entity<ValidacionDto>().HasNoKey();
            modelBuilder.Entity<UsuarioEstatus>().HasNoKey();
            modelBuilder.Entity<UsuarioInfoDto>().HasNoKey();
            modelBuilder.Entity<ControlUsuarioDto>().HasNoKey();
        }

    }
}
