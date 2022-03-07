using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SalesAppWebAPI.Models
{
    public partial class CustomerDBContext : DbContext
    {
        public CustomerDBContext()
        {
        }

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=CustomerDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnName("CreationTIme");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.VendorId, "IX_Products_VendorId");

                entity.Property(e => e.CreationTime).HasColumnName("CreationTIme");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VendorId);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.HasIndex(e => e.CustomerId, "IX_Sale_CustomerId");

                entity.HasIndex(e => e.ProductId, "IX_Sale_ProductId");

                entity.Property(e => e.CreationTime).HasColumnName("CreationTIme");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnName("CreationTIme");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
