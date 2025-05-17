using Domain;
using Domain.CompanyAggregate;
using Domain.DriverAggregate;
using Domain.RefreshToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        // Osnovna konfiguracija propertija
        builder.Property(rt => rt.Token)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(rt => rt.Expires)
            .IsRequired();

        builder.Property(rt => rt.UserAgent)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);

        // Konfiguracija za Value Object ConsumerIdentity
        builder.HasOne<User>().WithMany().HasForeignKey(rt => rt.UserId);

        // Dodatni indeksi
        builder.HasIndex(rt => rt.Expires);
        builder.HasIndex(rt => new { rt.Expires, rt.RevokedAt });

        // Ime tabele
    }
}
