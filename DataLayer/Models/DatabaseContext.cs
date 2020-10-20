using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().Property(s => s.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Type>().Property(t => t.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Client>().Property(c => c.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Client>().Property(c => c.Surname).HasColumnType("varchar(20)");
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal");
            modelBuilder.Entity<Transaction>().HasOne(t => t.Client).WithMany(c => c.Transactions);
        }
    }
}
