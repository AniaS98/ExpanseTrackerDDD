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
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);
            //Ustawienie klucza obcego
            builder.Property(a => a.Id).ValueGeneratedNever();
            //builder.Ignore(a => a.DomainEventPublisher);
            builder.Property<Guid>("AccountId").IsRequired();

            // Relacja 1:N pomiędzy Account i Budget ?
            builder.HasOne<Account>().WithMany().IsRequired(false).HasForeignKey("AccountId");

            builder.OwnsOne(b => b.CurrentValue);
            builder.OwnsOne(b => b.Limit);        
        }
    }
}
