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
        public async Task Post([FromBody] CreateCompanyCommand createCompany)
        {
            await _sender.Send(createCompany);
        }

    }
}
