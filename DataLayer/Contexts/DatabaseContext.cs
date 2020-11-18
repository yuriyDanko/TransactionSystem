using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Type = Entities.Entities.Type;

namespace DataLayer.Contexts
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Entities.Entities.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().Property(s => s.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Entities.Entities.Type>().Property(t => t.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Client>().Property(c => c.Name).HasColumnType("varchar(20)");
            modelBuilder.Entity<Client>().Property(c => c.Surname).HasColumnType("varchar(20)");
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal");
            modelBuilder.Entity<Transaction>().HasOne(t => t.Client).WithMany(c => c.Transactions).HasForeignKey(t => t.ClientId);
            modelBuilder.Entity<Transaction>().HasOne(t => t.Type).WithOne(t => t.Transaction).HasForeignKey<Type>(t => t.TransactionId);
            modelBuilder.Entity<Transaction>().HasOne(t => t.Status).WithOne(t => t.Transaction).HasForeignKey<Status>(s => s.TransactionId);


        }
    }
}
