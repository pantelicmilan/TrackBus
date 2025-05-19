using Application.Abstractions;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAuthContext _userAuthContext;

        public DeleteCompanyCommandHandler(
            ICompanyRepository companyRepository, 
            IUnitOfWork unitOfWork,
            IUserAuthContext userAuthContext
          )
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork; 
            _userAuthContext = userAuthContext;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var id = _userAuthContext.Id;
            var company = await _companyRepository.GetCompanyById(id);
            if (company == null) throw new CompanyNotExistException(id);
            _companyRepository.DeleteCompany(id);
            await _unitOfWork.SaveChanges();
            return Unit.Value;
            
        }
    }
}
