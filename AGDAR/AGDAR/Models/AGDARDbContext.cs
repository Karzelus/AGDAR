using Microsoft.EntityFrameworkCore;
using AGDAR.Models.DTO;

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
        //public DbSet<ProductCategory> ProductCategories { get; set; }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }

        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }

        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity :class, new()
        {
            Set<TEntity>().AddRange(entity);
        }

        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }


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
            modelBuilder.Entity<Role>()
                .Property(c => c.Name)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(c => c.Price)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property (c => c.Name)
                .IsRequired()
                .HasMaxLength (25);
            modelBuilder.Entity<Client>()
                .Property(c => c.SeckondName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Client>()
                .Property(c => c.DateOfBirth)
                .IsRequired();



            modelBuilder.Entity<OrderProduct>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(c => c.Order)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.OrderId);

            modelBuilder.Entity<PartProduct>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<PartProduct>()
                .HasOne(c => c.Part)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.PartId);

        }


        public DbSet<AGDAR.Models.DTO.WorkerDto>? WorkerDto { get; set; }


        public DbSet<AGDAR.Models.DTO.ClientDto>? ClientDto { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
