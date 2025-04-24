using Application.Abstractions;
using Domain.CompanyAggregate;
using Domain.DriverAggregate;
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
                new("companyUsername", company.CompanyUsername),
                new("userType", "company")
            };

        return GenerateToken(claims);
  
    }
    public string Generate(Driver driver)
    {
        throw new NotImplementedException();
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
