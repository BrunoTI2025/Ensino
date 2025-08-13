using Microsoft.EntityFrameworkCore;
using GestaoEstagios.Api.Models;

namespace GestaoEstagios.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Universidade> Universidades { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }
        public DbSet<PedidoEstagio> PedidosEstagio { get; set; }
        public DbSet<DocumentoPedido> DocumentosPedido { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Universidade>().ToTable("Universidades");
            modelBuilder.Entity<Estudante>().ToTable("Estudantes");
            modelBuilder.Entity<PedidoEstagio>().ToTable("PedidosEstagio");
            modelBuilder.Entity<DocumentoPedido>().ToTable("DocumentosPedido");
            modelBuilder.Entity<Utilizador>().ToTable("Utilizadores");
        }
    }
}
