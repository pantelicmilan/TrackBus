using DomainCompany = Domain.CompanyAggregate;
using Domain.Repositories;
using MediatR;
using Application.Abstractions;
using Domain.CompanyAggregate;
using Application.Exceptions;
namespace Application.Company.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashingProvider _hashingProvider;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IHashingProvider hashingProvider
    )
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _hashingProvider = hashingProvider;
    }

    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var existingCompany = await _companyRepository.GetCompanyByUsername(request.companyUsername);


        var company = DomainCompany.Company.Create(
                request.companyName,
                _hashingProvider.Hash(request.companyPassword),
                request.companyUsername
        );

        _companyRepository.CreateCompany(company);
        await _unitOfWork.SaveChanges();
        return company.Id;
    }
}
