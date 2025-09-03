using Microsoft.EntityFrameworkCore;
using dotnet_projects.Models;

namespace dotnet_projects.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureUserEntity(modelBuilder);
            ConfigureInventoryEntity(modelBuilder);
            ConfigureInventoryItemEntity(modelBuilder);
        }

        private static void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id).HasName("pk_users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Username).HasColumnName("username").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).HasColumnName("passwordhash").IsRequired();
                entity.Property(e => e.IsAdmin).HasColumnName("isadmin").HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.Username).IsUnique().HasDatabaseName("ix_users_username");
                entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("ix_users_email");   
                entity.HasMany(e => e.Inventories)
                    .WithOne(e => e.Owner)
                    .HasForeignKey(e => e.OwnerId)
                    .HasConstraintName("fk_inventories_users_ownerid")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureInventoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("inventories");
                entity.HasKey(e => e.Id).HasName("pk_inventories");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.OwnerId).HasColumnName("ownerid").IsRequired();
                entity.HasIndex(e => e.OwnerId).HasDatabaseName("ix_inventories_ownerid");
                entity.HasIndex(e => e.Name).HasDatabaseName("ix_inventories_name");
                entity.HasMany(e => e.InventoryItems)
                    .WithOne(e => e.Inventory)
                    .HasForeignKey(e => e.InventoryId)
                    .HasConstraintName("fk_inventoryitems_inventories_inventoryid")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureInventoryItemEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable("inventoryitems", table => 
                {
                    table.HasCheckConstraint("ck_inventoryitems_quantity", "quantity >= 0");
                    table.HasCheckConstraint("ck_inventoryitems_price", "price >= 0");
                });
                entity.HasKey(e => e.Id).HasName("pk_inventoryitems");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(1000);
                entity.Property(e => e.Quantity).HasColumnName("quantity").HasDefaultValue(0);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
                entity.Property(e => e.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.InventoryId).HasColumnName("inventoryid").IsRequired();
                entity.HasIndex(e => e.InventoryId).HasDatabaseName("ix_inventoryitems_inventoryid");
            });
        }
    }
}