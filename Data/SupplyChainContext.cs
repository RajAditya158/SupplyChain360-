using Microsoft.EntityFrameworkCore;
using Supplychain.Models.Warehouse;

namespace Supplychain.Data
{
    public class SupplyChainContext : DbContext
    {
        public SupplyChainContext(DbContextOptions<SupplyChainContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Inventory>().ToTable("Inventory");

            // Example: link Order.SKU to Inventory.SKU
            modelBuilder.Entity<Order>()
                .HasOne<Inventory>()
                .WithMany()
                .HasForeignKey(o => o.SKU)
                .HasPrincipalKey(i => i.SKU);
        }
    }
}
