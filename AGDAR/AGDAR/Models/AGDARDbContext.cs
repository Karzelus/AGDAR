using Microsoft.EntityFrameworkCore;

namespace AGDAR.Models
{
    public class AGDARDbContext : DbContext
    {
        //private string _connectionString = "Server =(localdb)\\mssqllocaldb;Database=AGDARDb;Trusted_Connection=True;";
        public AGDARDbContext(DbContextOptions<AGDARDbContext> options) : base(options)
        { 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Product>()
                .Property(c => c.Price)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(c => c.Name)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(c => c.Price)
                .IsRequired();

            modelBuilder.Entity<ProductCategory>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(c => c.ProductMany)
                .WithMany(c => c.Categorys)
                .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(c => c.CategoryMany)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(c => c.OrderMany)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.OrderId);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(c => c.ProductMany)
                .WithMany(c => c.Orders)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<PartProduct>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<PartProduct>()
                .HasOne(c => c.PartMany)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.PartId);
            modelBuilder.Entity<PartProduct>()
                .HasOne(c => c.ProductMany)
                .WithMany(c => c.Parts)
                .HasForeignKey(pc => pc.ProductId);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
