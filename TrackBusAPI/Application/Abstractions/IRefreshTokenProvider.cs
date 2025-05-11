using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IRefreshTokenProvider
{
    public string GenerateRandomToken();
    public void SetRefreshTokenAsACookie(string refreshToken);
}
