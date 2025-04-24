using Application.Abstractions;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;
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

    public AuthenticateCompanyCommandHandler(
        IJwtProvider jwtProvider, 
        ICompanyRepository companyRepository,
        IHashingProvider hashingProvider
    )
    {
        _jwtProvider = jwtProvider;
        _companyRepository = companyRepository;
        _hashingProvider = hashingProvider;
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
            return new AuthenticateCompanyResponse
            {
                JwtToken = _jwtProvider.Generate(companyByUsername)
            };
        }
        throw new ArgumentException("Invalid password");
    }

}
