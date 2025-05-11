using Application.Abstractions;
using Domain.RefreshToken;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Company.Commands.RefreshCompanyAuth;

public class RefreshCompanyAuthCommandHandler : IRequestHandler<RefreshCompanyAuthCommand, RefreshCompanyAuthResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IHashingProvider _hashingProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtProvider _jwtProvider;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public RefreshCompanyAuthCommandHandler(
        IRefreshTokenRepository refreshTokenRepository, 
        IHashingProvider hashingProvider, 
        IHttpContextAccessor httpContextAccessor,
        IJwtProvider jwtProvider, 
        ICompanyRepository companyRepository, 
        IUnitOfWork unitOfWork,
        IRefreshTokenProvider refreshTokenProvider
        )
    {
        _refreshTokenRepository = refreshTokenRepository;
        _hashingProvider = hashingProvider;
        _httpContextAccessor = httpContextAccessor;
        _jwtProvider = jwtProvider;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<RefreshCompanyAuthResponse> Handle(RefreshCompanyAuthCommand request, CancellationToken cancellationToken)
    {
        if (!_jwtProvider.IsJwtSignedByServer(request.jwtToken))
        {
            throw new UnauthorizedAccessException("Jwt is not signed by server!'");
        }

        var companyIdPayload = _jwtProvider.GetPayloadFromJwtToken(request.jwtToken, "sub");

        try
        {
            Convert.ToInt32(companyIdPayload);
        }
        catch (Exception ex) 
        {
            throw new Exception("valid companyId does not exist");
        }

        var companyId = Convert.ToInt32(companyIdPayload);

        var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        var refreshTokenCookie = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        Console.WriteLine(refreshTokenCookie);
        if (refreshTokenCookie == null) throw new ArgumentException("No refresh token!");

        var refreshTokens = await _refreshTokenRepository
            .GetAllRefreshTokenByCompanyId(companyId: companyId);
        if (refreshTokens.Count() == 0) throw new Exception("Refresh token does not exist!");

        var validRefreshToken = refreshTokens
            .FirstOrDefault(rt => _hashingProvider.IsPasswordValid(refreshTokenCookie, rt.Token));
        if (validRefreshToken == null) throw new UnauthorizedAccessException("Unauhorized Jwt Token!");
        if(validRefreshToken.UserAgent != userAgent)
        {
            validRefreshToken.Revoke();
            await _unitOfWork.SaveChanges();
            throw new UnauthorizedAccessException("You requested to refresh the token from another device");
        } 

        _refreshTokenRepository.RemoveRefreshToken(validRefreshToken);

        var generatedRefreshToken = _refreshTokenProvider.GenerateRandomToken();

        var company = await _companyRepository.GetCompanyById(companyId);

        if (company == null) throw new Exception("Company not found!");

        _refreshTokenRepository.AddRefreshToken(RefreshToken.CreateRefreshToken(
            ConsumerIdentity.CreateConsumerIdentity(null, companyId),
            userAgent,
            _hashingProvider.Hash(generatedRefreshToken)
        ));

        _refreshTokenProvider.SetRefreshTokenAsACookie(generatedRefreshToken);

        await _unitOfWork.SaveChanges();
        return new RefreshCompanyAuthResponse
        {
            JwtToken = _jwtProvider.Generate(company)
        };

    }
}
