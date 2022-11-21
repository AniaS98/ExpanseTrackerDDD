using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);

            //Ustawienie klucza obcego
            builder.Property(a => a.AccountId).ValueGeneratedNever();
            builder.Property<Guid>("AccountId").IsRequired();

            // Relacja 1:N pomiędzy Account i Transaction
            builder.HasOne<Account>().WithMany().IsRequired().HasForeignKey("AccountId");

            //Relacja 1:1 pomiędzy Transaction i Money
            builder.HasOne(t => t.Value);

            builder.Ignore(t => t.DomainEvents);
            builder.Ignore(t => t.IntegrationEvents);
        }
    }
}
