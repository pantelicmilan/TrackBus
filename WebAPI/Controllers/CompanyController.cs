using Application.Company.Commands.AuthenticateCompany;
using Application.Company.Commands.CreateCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    }
}
