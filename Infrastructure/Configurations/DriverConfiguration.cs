using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PratiBus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {

        // Configure the primary key
        builder.HasKey(d => d.Id);
       
        builder.HasOne<Company>()
             .WithMany()
            .HasForeignKey(d => d.CompanyId)

            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(d => d.DriverName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.DriverPassword)
            .IsRequired()
            .HasMaxLength(50);
    }
}
