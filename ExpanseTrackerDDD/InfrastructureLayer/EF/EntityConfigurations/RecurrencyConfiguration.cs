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
    public class RecurrencyConfiguration : IEntityTypeConfiguration<Recurrency>
    {
        public void Configure(EntityTypeBuilder<Recurrency> builder)
        {
            //Sztuczne pole Id jako klucz główny
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");
        }
    }
}
