using Application.Company.Commands.AuthenticateCompany;
using Application.Company.Commands.CreateCompany;
using Application.Company.Commands.RefreshCompanyAuth;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ISender _sender;
        public CompanyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<int> CreateCompany([FromBody] CreateCompanyCommand createCompany)
        {
            int result = await _sender.Send(createCompany);
            return result;
        }

        [HttpPost("/auth")]
        public async Task<AuthenticateCompanyResponse> Login([FromBody] AuthenticateCompanyCommand authCompany) 
        {
            var result = await _sender.Send(authCompany);
            return result;
        }

        [HttpPost("/refresh")]
        public async Task<RefreshCompanyAuthResponse> RefreshAuth([FromBody] RefreshCompanyAuthCommand refreshCompany) 
        {
            var result = await _sender.Send(refreshCompany);
            return result;
        }

        [Authorize(Policy = PolicyList.AdminPolicy)]
        [HttpGet("/test")]
        public async Task<string> TestAuthorization()
        {
            return "sss";
        }

    }
}
