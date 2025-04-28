using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Company.Commands.AuthenticateCompany;

public record AuthenticateCompanyCommand(string companyUsername, string companyPassword) : IRequest<AuthenticateCompanyResponse>;
