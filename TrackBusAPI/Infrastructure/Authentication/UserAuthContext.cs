using Application.Abstractions;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;

public class UserAuthContext : IUserAuthContext
{
    private readonly IHttpContextAccessor _contextAccessor;
    public UserAuthContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public int Id{ 
        get {
            var subClaim = _contextAccessor.HttpContext.User?.FindFirst(JwtClaimsList.Sub)?.Value;
            if (string.IsNullOrEmpty(subClaim) || !int.TryParse(subClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Unauthorized Access");
            }
            return userId;
        }
    }

    public string UserName
    {
        get
        {
            var username = _contextAccessor.HttpContext.User?.FindFirst(JwtClaimsList.Username)?.Value;
            if (username is null) throw new Exception("Username not found!");
            return username;
        }
    }
    public Role Role {
        get {
            var httpRole = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            if (httpRole is null) throw new Exception("Invalid role!");

            if (Enum.TryParse<Role>(httpRole, ignoreCase: true, out Role role))
            {
                return role;
            }

            throw new Exception("Invalid role");
        }
    
    } 

    public string GetHeaderValue(string httpHeaderKey)
    {
        return _contextAccessor.HttpContext?.Request.Headers[httpHeaderKey].ToString();
    }

    public string GetRefreshToken()
    {
        var refreshToken =  _contextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        if (refreshToken is null) throw new Exception("Refresh token not found!");
        return refreshToken;
    }

    public string GetUserAgentValue()
    {
        var userAgent =  GetHeaderValue("User-Agent");
        if (userAgent is null) throw new Exception("User agent is null");
        return userAgent;
    }
}
