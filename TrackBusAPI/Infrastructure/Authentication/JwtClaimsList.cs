using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;

public static class JwtClaimsList
{
    public static string Username = "username";
    public static string UserType = "userType";
    public static string Sub = System.Security.Claims.ClaimTypes.NameIdentifier;
}
