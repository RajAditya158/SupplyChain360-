using Microsoft.EntityFrameworkCore;
using Supplychain.Models.Warehouse;
using SupplyChain360.Models.Notifications;

namespace Supplychain.Data
{
    public class SupplyChainContext : DbContext
    {
        public SupplyChainContext(DbContextOptions<SupplyChainContext> options)
            : base(options)
        {
        }

        //  Your tables
        public DbSet<Inventory> Inventory { get; set; }

        // Other tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<InboundShipment> InboundShipments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
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

            modelBuilder.Entity<LoginResponse>().HasNoKey();
            //  Existing tables (already in DB)
            modelBuilder.Entity<Inventory>()
                .ToTable("Inventory")
                .Metadata.SetIsTableExcludedFromMigrations(true);

            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .Metadata.SetIsTableExcludedFromMigrations(true);  //  ADD THIS

            //  Other mappings (new tables)
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

            // Relationship
            modelBuilder.Entity<Order>()
                .HasOne<Inventory>()
                .WithMany()
                .HasForeignKey(o => o.SKU)
                .HasPrincipalKey(i => i.SKU);

            //  ADD THIS (Shipment → PurchaseOrder FK)
                modelBuilder.Entity<InboundShipment>()
                    .HasOne<PurchaseOrder>()
                    .WithMany()
                    .HasForeignKey(s => s.PoId);
                    //Procurement relationships

        //supplier unique constraint
            modelBuilder.Entity<Supplier>()
        .HasIndex(s => new { s.SupplierId, s.Name })
        .IsUnique();
        //purchaseorder → supplier
        modelBuilder.Entity<PurchaseOrder>()
        .HasOne<Supplier>()
        .WithMany()
        .HasForeignKey(po => new { po.SupplierId, po.SupplierName })
        .HasPrincipalKey(s => new { s.SupplierId, s.Name }) // Links to Supplier's Id and Name
        .OnDelete(DeleteBehavior.Restrict);  
//notification → user
            modelBuilder.Entity<Notification>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

    // OrderItem -> PurchaseOrder Relationship
    modelBuilder.Entity<OrderItem>()
        .HasOne<PurchaseOrder>()       // Reference the class even if property is missing
        .WithMany()                    // A PurchaseOrder can have many items
        .HasForeignKey(oi => oi.PoId)  // The actual ID column
        .OnDelete(DeleteBehavior.Cascade);
    }
    }
}
