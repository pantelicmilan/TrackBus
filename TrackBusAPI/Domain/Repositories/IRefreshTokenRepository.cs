namespace Domain.Repositories;

public interface IRefreshTokenRepository
{
    public void AddRefreshToken(RefreshToken.RefreshToken refreshToken);
    public Task<RefreshToken.RefreshToken> GetRefreshTokenByHash(string refreshTokenHash);
    public Task<List<RefreshToken.RefreshToken>> GetAllRefreshTokenByCompanyId(int companyId);
    public void RemoveRefreshToken(RefreshToken.RefreshToken refreshToken);
}