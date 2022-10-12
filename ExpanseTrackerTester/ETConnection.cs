using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using ReportCreator.InfrastructureLayer.EF;

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

            var context = new RCContext(options);

            return context;

        }

    }
}
