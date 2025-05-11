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
        builder.OwnsOne(rt => rt.ConsumerIdentity, ci =>
        {
            // Mapiranje kolona
            ci.Property(c => c.DriverId)
                .HasColumnName("DriverId")
                .IsRequired(false);

            ci.Property(c => c.CompanyId)
                .HasColumnName("CompanyId")
                .IsRequired(false);

            // Relacija ka Driver entitetu
            ci.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(c => c.DriverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacija ka Company entitetu
            ci.HasOne<Company>()
                .WithMany()
                .HasForeignKey(c => c.CompanyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Indeksi
            ci.HasIndex(c => c.DriverId);
            ci.HasIndex(c => c.CompanyId);
        });

        // Dodatni indeksi
        builder.HasIndex(rt => rt.Expires);
        builder.HasIndex(rt => new { rt.Expires, rt.RevokedAt });

        // Ime tabele
    }
}
