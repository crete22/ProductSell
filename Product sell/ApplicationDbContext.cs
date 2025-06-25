using Microsoft.EntityFrameworkCore;

namespace Product_sell
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductInOrder> ProductInOrders { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Новая строка подключения для PostgreSQL.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductSellDB;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Login)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Login)
                .IsUnique();
        }
    }
}