using Domain.CompanyAggregate;
using Domain.DriverAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(c => c.CompanyName).HasMaxLength(50);

        builder.HasMany<Driver>()
               .WithOne()
               .HasForeignKey(d => d.CompanyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
