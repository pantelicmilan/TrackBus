using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Company.Commands.DeleteCompany
{
    public record DeleteCompanyCommand(int companyId) : IRequest<Unit>;
}
