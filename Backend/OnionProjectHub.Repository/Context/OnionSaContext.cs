using Microsoft.EntityFrameworkCore;
using OnionProjectHub.Domain.Models;


namespace OnionProjectHub.Repository.Context
{
    public class OnionSaContext : DbContext
    {
        public OnionSaContext(DbContextOptions<OnionSaContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().HasKey(c => c.CPFCNPJ);
        }

    }
}
