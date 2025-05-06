using Application.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCompanyCommandHandler(
            ICompanyRepository companyRepository, 
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork; 
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
    
            var company = await _companyRepository.GetCompanyById(request.companyId);
            if (company == null) throw new CompanyNotExistException(request.companyId);
            _companyRepository.DeleteCompany(request.companyId);
            await _unitOfWork.SaveChanges();
            return Unit.Value;
            
        }
    }
}
