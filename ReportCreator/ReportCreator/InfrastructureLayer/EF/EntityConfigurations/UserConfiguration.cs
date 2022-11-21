using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
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
