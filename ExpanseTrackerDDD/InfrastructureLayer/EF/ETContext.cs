using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.InfrastructureLayer.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF

{
    public class ETContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public ETContext(DbContextOptions<ETContext> options) : base(options) 
        {
            //string basePath = @"D:\Ania\Documents\STUDIA\_Magisterka\_Praca Magisterska\ExpanseTrackerDDD\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            string basePath = @"C:\Users\AnnaSzmit\Documents\My project\Program\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            if (File.Exists(Path.Combine(basePath,"ExpanseTrackerDDD_Base.db")))
            {
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db"));
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db-shm"));
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db-wal"));
            }

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new BudgetConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new RecurrencyConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new MoneyConfiguration());
        }
    }
}
