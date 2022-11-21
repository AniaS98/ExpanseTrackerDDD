using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);

            //Ustawienie klucza obcego
            builder.Property(a => a.OwnerId).ValueGeneratedNever();
            builder.Property<Guid>("OwnerId").IsRequired();

            //Relacja 1:1 pomiędzy Account i Money
            builder.HasOne(a => a.AccountBalance);
            builder.HasOne(a => a.Overdraft);

            // Relacja 1:N pomiędzy User i Account
            builder.HasOne<User>().WithMany().IsRequired().HasForeignKey("OwnerId");

            builder.Ignore(a => a.DomainEvents);
            builder.Ignore(a => a.IntegrationEvents);
        }
    }
}
