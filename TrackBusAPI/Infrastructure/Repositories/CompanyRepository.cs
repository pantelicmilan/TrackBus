using Domain.CompanyAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //
    public void CreateCompany(Company company)
    {
        _context.Company.Add(company);
    }

    //
    public void DeleteCompany(int companyId)
    {
        var company = _context.Company.FirstOrDefault(c => c.Id == companyId);
        if (company != null)
        {
            _context.Company.Remove(company);
        }
    }

    //
    public async Task<Company> GetCompanyById(int companyId)
    {
        return await _context.Company.FirstOrDefaultAsync(c => c.Id == companyId);
    }

    public async Task<Company> GetCompanyByUsername(string username)
    {
        var company = await _context.Company.FirstOrDefaultAsync(c => c.CompanyUsername == username);
        return company;
    }

    //
    public async Task UpdateCompany(Company company)
    {
        var existingCompany = await _context.Company.FirstOrDefaultAsync(c => c.Id == company.Id);
        if (existingCompany != null)
        {
            existingCompany.UpdateCompanyName(company.CompanyName);
            existingCompany.UpdateCompanyUsername(company.CompanyUsername);
            existingCompany.UpdateCompanyPassword(company.CompanyPassword);
            _context.Company.Update(existingCompany);
        }
    }

}
