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
            var userContext = _httpContextAccessor.HttpContext?.User;
            if (userContext == null)
            {
                throw new UnauthorizedAccessException("Unauthorized Access");
            }

            foreach (var claim in userContext.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            var subClaim = userContext.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            
            Console.WriteLine("ovo ovo: "+subClaim);

            if (string.IsNullOrEmpty(subClaim) || !int.TryParse(subClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Unauthorized Access");
            }
            var company = await _companyRepository.GetCompanyById(userId);
            if (company == null) throw new CompanyNotExistException(userId);
            _companyRepository.DeleteCompany(userId);
            await _unitOfWork.SaveChanges();
            return Unit.Value;
            
        }
    }
}
