using Application.Abstractions;
using Domain.CompanyAggregate;
using Domain.DriverAggregate;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;
public class JwtOptions
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryHours { get; set; }
}

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly SymmetricSecurityKey _securityKey;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
    }

    public string Generate(Company company)
    {
     
        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, company.Id.ToString()),
                new(JwtClaimsList.CompanyUsername, company.CompanyUsername),
                new(JwtClaimsList.UserType, "company"),
                new(ClaimTypes.Role, Role.Admin)
            };

        return GenerateToken(claims);
  
    }
    public string Generate(Driver driver)
    {
        throw new NotImplementedException();
    }

    public string GetPayloadFromJwtToken(string jwtToken, string propertyName)
    {
        // Kreirajte JwtSecurityTokenHandler za parsiranje JWT tokena
        var handler = new JwtSecurityTokenHandler();

        // Parsirajte token
        var token = handler.ReadJwtToken(jwtToken);

        // Vratite payload kao string (JSON)
        if (token.Payload.TryGetValue(propertyName, out var propertyValue))
        {
            // Vratite vrednost specifičnog property-ja kao string
            return propertyValue?.ToString();
        }

        // Ako property nije pronađen, vratite null ili neku drugu vrednost
        return null;
    }

    public bool IsJwtSignedByServer(string jwt)
    {
        try
        {
            // Kreiraj JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            // Postavljanje security key-a koji će biti korišćen za verifikaciju
            var key = Encoding.ASCII.GetBytes(_options.SecretKey);

            // Postavljanje parametara za validaciju
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Ako želite, možete validirati issuer
                ValidateAudience = false, // Ako želite, možete validirati audience
               // ValidateLifetime = true, // Validacija vremena isteka tokena
                IssuerSigningKey = new SymmetricSecurityKey(key) // Koristi isti key koji je korišćen za potpisivanje
            };

            // Pokušaj validacije tokena
            var principal = handler.ValidateToken(jwt, validationParameters, out var validatedToken);

            // Ako je token validan, vraća true
            return true;
        }
        catch (SecurityTokenExpiredException)
        {
            Console.WriteLine("Token je istekao.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token nije validan: {ex.Message}");
            return false;
        }
    }

    private string GenerateToken(List<Claim> claims)
    {
        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpiryHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
