using ExpanseTrackerDDD.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);

            //Ustawienie klucza obcego
            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property<Guid>("UserId").IsRequired();

            //Relacja 1:1 pomiędzy Account i Money (2x)
            builder.HasOne(a => a.Balance);
            builder.HasOne(a => a.Overdraft);

            // Relacja 1:N pomiędzy User i Account
            builder.HasOne<User>().WithMany().IsRequired(true).HasForeignKey("UserId");

            builder.Ignore(a => a.DomainEvents);
            builder.Ignore(a => a.IntegrationEvents);
        }
    }
}
