using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IHashingProvider
{
    public string Hash(string plainTextValue);
    public bool IsPasswordValid(string plainText, string hashedPassword);
}
