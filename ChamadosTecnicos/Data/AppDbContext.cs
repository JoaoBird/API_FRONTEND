using Microsoft.EntityFrameworkCore;
using ChamadosTecnicos.Models;

namespace ChamadosTecnicos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Problema> Problemas { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}