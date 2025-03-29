using Microsoft.EntityFrameworkCore;

namespace ErasmusProject
{
    public class Context :DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {

        }
        public Context()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=RAWAN;Database=Ecommerce;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.ShoppingCarts)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(s => s.CartDetails)
                .WithOne(s => s.ShoppingCart)
                .HasForeignKey(s => s.CartId)
                .IsRequired();

            modelBuilder.Entity<CartDetail>()
                .HasMany(c => c.Products)
                .WithOne(c => c.CartDetail)
                .HasForeignKey(c => c.ProductId)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired();

          
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Order>(o => o.PaymentId)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(p => p.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .IsRequired();

            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Products)
                .WithOne(a => a.Admin)
                .HasForeignKey(a => a.AdminId)
                .IsRequired();

            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Payments)
                .WithOne(a => a.Admin)
                .HasForeignKey(a => a.AdminId)
                .IsRequired();

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.ProductId, op.OrderId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.OrderId);
        }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        }
    
}

