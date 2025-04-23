using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Company.Commands.CreateCompany;

public record CreateCompanyCommand(string companyName, string companyUsername, string companyPassword) : IRequest;
