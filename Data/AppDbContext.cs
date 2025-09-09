using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Entities.Cart;

namespace TinyFeetBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product ↔ Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Cart ↔ CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Toys" },
                new Category { Id = 2, Name = "Kids Accessories" },
                new Category { Id = 3, Name = "Clothing" }
            );

            // Order ↔ OrderItems
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);


            // Order ↔ Payment (One-to-One)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            // Order ↔ User (One User can have many Orders)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // Payment configurations
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(p => p.PaidAt)
                      .HasColumnType("datetime2")
                      .IsRequired();

                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.PaymentMethod)
                      .HasMaxLength(50);

                entity.Property(p => p.Status)
                      .HasMaxLength(20);

                entity.Property(p => p.TransactionId)
                      .HasMaxLength(100);
            });
        }
    }
}
