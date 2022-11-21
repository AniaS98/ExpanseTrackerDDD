using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);

            //Ustawienie klucza obcego
            builder.Property(a => a.AccountId).ValueGeneratedNever();
            builder.Property<Guid>("AccountId").IsRequired();

            builder.Ignore(b => b.DomainEvents);
            builder.Ignore(b => b.IntegrationEvents);
        }

    }
}
