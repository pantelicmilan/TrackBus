using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;

public class RefreshTokenProvider : IRefreshTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RefreshTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor  = httpContextAccessor;
    }
    public string GenerateRandomToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[40];
        rng.GetBytes(randomBytes);
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    public void SetRefreshTokenAsACookie(string refreshToken)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append(
            "refreshToken",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                    // mora biti false jer nije HTTPS
                SameSite = SameSiteMode.Lax,      // Lax je podrazumevan, radi sa većinom form-submita
                Expires = DateTime.UtcNow.AddDays(7),

            }
        );
    }
}
