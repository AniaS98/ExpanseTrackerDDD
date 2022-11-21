using Microsoft.EntityFrameworkCore;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF.EntityConfigurations;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF
{
    public class RCContext :DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public RCContext(DbContextOptions<RCContext> options) : base(options)
        {
            string basePath = @"C:\Users\AnnaSzmit\Documents\My project\Program\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            if (File.Exists(Path.Combine(basePath, "ReportCreator_Base.db")))
            {
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db"));
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db-shm"));
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db-wal"));
            }

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ReportConfiguration());
            builder.ApplyConfiguration(new MoneyConfiguration());
            builder.ApplyConfiguration(new BudgetConfiguration());
        }


    }
}
