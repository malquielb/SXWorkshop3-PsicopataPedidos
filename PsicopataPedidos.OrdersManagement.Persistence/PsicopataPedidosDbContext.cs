using Microsoft.EntityFrameworkCore;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Persistence
{
    public class PsicopataPedidosDbContext : DbContext
    {
        public PsicopataPedidosDbContext(DbContextOptions<PsicopataPedidosDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PsicopataPedidosDbContext).Assembly);

            modelBuilder.Entity<Category>()
                .HasKey("Id");

            modelBuilder.Entity<Category>()
                .Property("Name")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products).WithMany(p => p.Categories);

            modelBuilder.Entity<Product>()
                .HasKey("Id");

            modelBuilder.Entity<Product>()
               .Property("Name")
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<Product>()
               .Property("Description")
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<Product>()
               .Property("Price")
               .HasColumnType("decimal(19,4)")
               .IsRequired();

            modelBuilder.Entity<Product>()
               .Property("Stock")
               .IsRequired();

            modelBuilder.Entity<Product>()
               .HasMany(p => p.Categories).WithMany(c => c.Products);

            modelBuilder.Entity<ShoppingListItem>()
                .HasKey("Id");

            modelBuilder.Entity<ShoppingListItem>()
               .Property(s => s.ProductId)
                .IsRequired();

            modelBuilder.Entity<ShoppingListItem>()
                .HasOne(s => s.Product);

            modelBuilder.Entity<ShoppingListItem>()
                .Property(s => s.Quantity)
                .IsRequired();

            modelBuilder.Entity<ShoppingListItem>()
                .Property(s => s.UserId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasKey("Id");

            modelBuilder.Entity<Order>()
                .Property("Total")
                .HasColumnType("decimal(19,4)");

            modelBuilder.Entity<Order>()
                .Property(o => o.Date)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ShoppingList);

            modelBuilder.Entity<Order>()
                .Property(o => o.UserId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.UserName)
                .IsRequired();
        }
    }
}
