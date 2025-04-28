using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using Bc = BCrypt.Net;
namespace Infrastructure.Authentication;

public class HashingProvider : IHashingProvider
{
    public string Hash(string plainTextValue)
    {
        return Bc.BCrypt.HashPassword(plainTextValue);
    }

    public bool IsPasswordValid(string plainText, string hashedPassword)
    {
        return Bc.BCrypt.Verify(plainText, hashedPassword);
    }
}
