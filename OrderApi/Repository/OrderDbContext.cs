using Microsoft.EntityFrameworkCore;
using OrderApi.Repository.Entities;

namespace OrderApi.Repository;

public class OrderDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Buyer> Buyers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDb;Database=BasicSellingDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        modelBuilder.Entity<Buyer>().HasKey(b => b.Id);
        modelBuilder.Entity<OrderProduct>().HasKey(b => new { b.OrderId, b.ProductId });

        //modelBuilder.Entity<Buyer>()
        //    .HasMany<Order>()
        //    .WithOne(o => o.Buyer)
        //    .HasForeignKey(o => o.BuyerId);


        //modelBuilder.Entity<Product>()
        //    .HasMany<OrderProduct>()
        //    .WithOne(o => o.Product)
        //    .HasForeignKey(o => o.ProductId);

        //modelBuilder.Entity<Order>()
        //    .HasMany<OrderProduct>()
        //    .WithOne(o => o.Order)
        //    .HasForeignKey(o => o.OrderId);
    }
}