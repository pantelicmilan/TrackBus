using Domain;
using Domain.CompanyAggregate;
using Domain.DriverAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Username).IsRequired().HasMaxLength(255);
        builder
            .Property(d => d.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.Username).IsUnique();


        builder.HasDiscriminator<string>("Discriminator")
                .HasValue<Company>("Company")
                .HasValue<Driver>("Driver");
    }
}
