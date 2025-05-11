using Domain.RefreshToken;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;
    public RefreshTokenRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshToken.Add(refreshToken);
    }

    public async Task<List<RefreshToken>> GetAllRefreshTokenByCompanyId(int companyId)
    {
        var refreshTokens = await _context.RefreshToken
            .Where(rt => rt.ConsumerIdentity.CompanyId == companyId)
            .ToListAsync();
        return refreshTokens;
    }

    public async Task<RefreshToken> GetRefreshTokenByHash(string refreshTokenHash)
    {
        var avRefreshToken = await _context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == refreshTokenHash);
        return avRefreshToken;
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshToken.Remove(refreshToken);
    }
}
