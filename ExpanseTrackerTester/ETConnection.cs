using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using ReportCreator.InfrastructureLayer.EF;
using System.IO;

namespace Tester
{
    public class ETConnection
    {
        public static ETContext InitializeExpanseTrackerContext()
        {
            //Baza SQLite
            var sqliteConnectionString = @"Data Source=ExpanseTrackerDDD_Base.db";
            var options = new DbContextOptionsBuilder<ETContext>()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))  // umożliwia m.in. podglądanie SQL generowanego przez EF
                .UseSqlite(sqliteConnectionString)
                .Options;

            //string basePath = @"D:\Ania\Documents\STUDIA\_Magisterka\_Praca Magisterska\ExpanseTrackerDDD\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            string basePath = @"C:\Users\AnnaSzmit\Documents\My project\Program\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";

            if (File.Exists(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db")))
            {
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db"));
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db-shm"));
                File.Delete(Path.Combine(basePath, "ExpanseTrackerDDD_Base.db-wal"));
            }

            var context = new ETContext(options);

            return context;

        }
        public static RCContext InitializeReportCreatorContext()
        {
            //Baza SQLite
            var sqliteConnectionString = @"Data Source=ReportCreator_Base.db";
            var options = new DbContextOptionsBuilder<RCContext>()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))  // umożliwia m.in. podglądanie SQL generowanego przez EF
                .UseSqlite(sqliteConnectionString)
                .Options;

            string basePath = @"C:\Users\AnnaSzmit\Documents\My project\Program\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            if (File.Exists(Path.Combine(basePath, "ReportCreator_Base.db")))
            {
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db"));
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db-shm"));
                File.Delete(Path.Combine(basePath, "ReportCreator_Base.db-wal"));
            }

            var context = new RCContext(options);

            return context;

        }

    }
}
