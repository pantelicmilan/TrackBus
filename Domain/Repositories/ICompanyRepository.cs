using Domain.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICompanyRepository
{
    public Task<Company> GetCompanyById(int companyId);
    public void CreateCompany(Company company);
    public void DeleteCompany(int companyId);
    public void UpdateCompany(Company company);

    // New methods to be implemented

}
