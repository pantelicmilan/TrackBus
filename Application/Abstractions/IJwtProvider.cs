using CompanyDomain = Domain.CompanyAggregate;
using Domain.DriverAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IJwtProvider
{
    public string Generate(CompanyDomain.Company company);
    public string Generate(Driver driver);
}
