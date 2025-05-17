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

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
       
        builder.HasOne<Company>()
             .WithMany()
            .HasForeignKey(d => d.CompanyId)

            .OnDelete(DeleteBehavior.Cascade);
    
    }
}
