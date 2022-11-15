using BaseDDD.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
{
    public class MoneyConfiguration : IEntityTypeConfiguration<Money>
    {
        public void Configure(EntityTypeBuilder<Money> builder)
        {
            //Sztuczne pole Id jako klucz główny
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");
        }
    }
}
