using Domain.Primitives;
using PratiBus.Primitives;

namespace PratiBus.Domain.Entities;

public class Company : AggregateRoot
{
    public string CompanyName { get; private set; }
    public string CompanyUsername { get; private set; }
    public string CompanyPassword { get; private set; }

    private Company(string companyName, string companyPassword, string companyUsername)
    {
        CompanyName = companyName;
        CompanyPassword = companyPassword;
        CompanyUsername = companyUsername;
    }

    public static Company Create(string companyName, string companyPassword, string companyUsername)
    {
        //1. validacija
        return new Company(companyName, companyPassword, companyUsername);
    }

    public void UpdateCompanyName(string newCompanyName)
    {
        if (string.IsNullOrWhiteSpace(newCompanyName))
            throw new ArgumentException("Company name cannot be empty.", nameof(newCompanyName));

        CompanyName = newCompanyName;
    }

    public void UpdateCompanyUsername(string newCompanyUsername)
    {
        if (string.IsNullOrWhiteSpace(newCompanyUsername))
            throw new ArgumentException("Company username cannot be empty.", nameof(newCompanyUsername));

        CompanyUsername = newCompanyUsername;
    }

    public void UpdateCompanyPassword(string newCompanyPassword)
    {
        if (string.IsNullOrWhiteSpace(newCompanyPassword))
            throw new ArgumentException("Company password cannot be empty.", nameof(newCompanyPassword));

        CompanyPassword = newCompanyPassword;
    }
}
