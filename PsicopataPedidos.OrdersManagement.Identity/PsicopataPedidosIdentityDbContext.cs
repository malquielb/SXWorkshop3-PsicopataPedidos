using Microsoft.EntityFrameworkCore;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity
{
    public class PsicopataPedidosIdentityDbContext : DbContext
    {
        public PsicopataPedidosIdentityDbContext(DbContextOptions<PsicopataPedidosIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PsicopataPedidosIdentityDbContext).Assembly);

            modelBuilder.Entity<User>()
                .HasKey("Id");

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.IsAdmin)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Wallet)
                .HasColumnType("decimal(19,4)");
        }
    }
}
