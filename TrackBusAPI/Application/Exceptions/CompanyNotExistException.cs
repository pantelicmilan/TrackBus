using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions;

public class CompanyNotExistException : Exception
{
    public CompanyNotExistException(int companyId)
    : base($"Company with id: '{companyId}' does not exist.")
    {
    }
}
