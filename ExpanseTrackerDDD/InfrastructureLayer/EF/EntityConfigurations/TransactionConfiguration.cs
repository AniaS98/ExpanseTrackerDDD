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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);
            //Ustawienie klucza obcego
            builder.Property(a => a.Id).ValueGeneratedNever();
            //builder.Ignore(a => a.DomainEventPublisher);
            builder.Property<Guid>("AccountId").IsRequired();

            // Relacja 1:N pomiędzy Account i Transaction
            builder.HasOne<Account>().WithMany().IsRequired().HasForeignKey("AccountId");

            //builder.Property<Recurrency>("TransactionRecurrency").IsRequired(false);
            //builder.Property<Category>("TransactionCategory").IsRequired(false);

            builder.HasOne(t => t.TransactionCategory);
            builder.HasOne(t => t.TransactionRecurrency);
            builder.HasOne(t => t.Value);

        }
    }
}
