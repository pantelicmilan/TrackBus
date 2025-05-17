using Domain.Primitives;
using PratiBus.Primitives;

namespace Domain.CompanyAggregate;

public class Company : User
{
    public string CompanyName { get; private set; }
    protected Company() { }
    private Company(string companyName, string companyPassword, string companyUsername) : base(companyUsername, companyPassword)
    {
        CompanyName = companyName;
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

        Username = newCompanyUsername;
    }

    public void UpdateCompanyPassword(string newCompanyPassword)
    {
        if (string.IsNullOrWhiteSpace(newCompanyPassword))
            throw new ArgumentException("Company password cannot be empty.", nameof(newCompanyPassword));

        Password = newCompanyPassword;
    }
}
