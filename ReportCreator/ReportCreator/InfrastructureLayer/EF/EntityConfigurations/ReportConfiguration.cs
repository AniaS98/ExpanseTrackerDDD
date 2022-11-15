using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF.EntityConfigurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            //Sztuczne pole Id jako klucz główny
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");

            //Ustawienie klucza obcego
            builder.Property(a => a.OwnerId).ValueGeneratedNever();
            builder.Property<Guid>("OwnerId").IsRequired();

            // Relacja 1:N pomiędzy User i Account
            builder.HasOne<User>().WithMany().IsRequired().HasForeignKey("OwnerId");
        }

    }
}
