using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpanseTrackerDDD.InfrastructureLayer.Data
{
    public class ETContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public ETContext() { }

        public ETContext(DbContextOptions<ETContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EFTrail;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("domainEventPublisher", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Account>().ToTable("domainEventPublisher", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Transaction>().ToTable("domainEventPublisher", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Budget>().ToTable("domainEventPublisher", t => t.ExcludeFromMigrations());
        }

    }
}