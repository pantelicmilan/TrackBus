using Application.Abstractions;
using Application.Exceptions;
using Domain.RefreshToken;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Company.Commands.AuthenticateCompany;

public class AuthenticateCompanyCommandHandler : IRequestHandler<AuthenticateCompanyCommand, AuthenticateCompanyResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly ICompanyRepository _companyRepository;
    private readonly IHashingProvider _hashingProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public AuthenticateCompanyCommandHandler(
        IJwtProvider jwtProvider,
        ICompanyRepository companyRepository,
        IHashingProvider hashingProvider,
        IHttpContextAccessor httpContextAccessor,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IRefreshTokenProvider refreshTokenProvider
    )
    {
        _jwtProvider = jwtProvider;
        _companyRepository = companyRepository;
        _hashingProvider = hashingProvider;
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<AuthenticateCompanyResponse> Handle(AuthenticateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyByUsername = await _companyRepository.GetCompanyByUsername(request.companyUsername);
        if (companyByUsername == null) 
        {
            throw new UsernameNotExistException(request.companyUsername);
        }

        
        if(_hashingProvider.IsPasswordValid(request.companyPassword, companyByUsername.CompanyPassword))
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var authResponse = new AuthenticateCompanyResponse
            {
                JwtToken = _jwtProvider.Generate(companyByUsername)
            };

            var generatedRefreshToken = _refreshTokenProvider.GenerateRandomToken();
            
            var createdRefreshToken = RefreshToken.CreateRefreshToken(
                    ConsumerIdentity.CreateConsumerIdentity(null, companyByUsername.Id),
                    userAgent,
                    _hashingProvider.Hash(generatedRefreshToken)
            );

            _refreshTokenRepository.AddRefreshToken(
                createdRefreshToken
            );

            _refreshTokenProvider.SetRefreshTokenAsACookie(
                generatedRefreshToken
             );

            await _unitOfWork.SaveChanges();

            return authResponse;
        }

        throw new ArgumentException("Invalid password");
    }

}
