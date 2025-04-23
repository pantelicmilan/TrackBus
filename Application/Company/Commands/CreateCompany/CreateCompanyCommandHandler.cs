using DomainCompany = Domain.CompanyAggregate;
using Domain.Repositories;
using MediatR;
using Application.Abstractions;
namespace Application.Company.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
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
    }

    public async Task Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        _companyRepository.CreateCompany(
            DomainCompany.Company.Create(
                request.companyName,
                _hashingProvider.Hash(request.companyPassword),
                request.companyUsername
            ));
        await _unitOfWork.SaveChanges();
    }
}
