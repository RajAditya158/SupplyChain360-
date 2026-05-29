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

        //  Your tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        // Other tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // Compliance tables
        // public DbSet<ComplianceReport> ComplianceReports { get; set; }
        // public DbSet<KPITracking> KPITrackings { get; set; }
        // public DbSet<KPIReport> KPIReports { get; set; }
        // public DbSet<KPIMetric> KPIMetrics { get; set; }

        // //Compliance tables
        // public DbSet<ComplianceReport> ComplianceReports { get; set; }

        // public DbSet<KPITracking> KPITrackings { get; set; }
        // public DbSet<KPIReport> KPIReports { get; set; }

        // public DbSet<KPIMetric> KPIMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Existing tables (already in DB)
            modelBuilder.Entity<Inventory>()
                .ToTable("Inventory")
                .Metadata.SetIsTableExcludedFromMigrations(true);

            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .Metadata.SetIsTableExcludedFromMigrations(true);  // ✅ ADD THIS

            // ✅ Other mappings (new tables)
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Supplier>()
                .Property(s => s.Status)
                .HasConversion<string>();
            modelBuilder.Entity<Warehouse>().ToTable("Warehouse");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLog");

            // modelBuilder.Entity<ComplianceReport>().ToTable("ComplianceReport");
            // modelBuilder.Entity<KPITracking>().ToTable("KPITracking");
            // modelBuilder.Entity<KPIReport>().ToTable("KPIReport");
            // modelBuilder.Entity<KPIMetric>().ToTable("KPIMetric");

            // ✅ Relationship
            modelBuilder.Entity<Order>()
                .HasOne<Inventory>()
                .WithMany()
                .HasForeignKey(o => o.SKU)
                .HasPrincipalKey(i => i.SKU);
        }
    }
}