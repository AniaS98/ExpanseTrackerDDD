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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Ustawienie klucza głównego
            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvents);
            builder.Ignore(a => a.IntegrationEvents);

        }
    }
}
