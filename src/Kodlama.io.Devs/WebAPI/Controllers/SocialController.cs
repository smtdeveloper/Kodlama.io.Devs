using Application.Features.Socials.Queries;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SocialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest  pageRequest)
        {
            GetListSocialsQuery socialsQuery = new() { PageRequest = pageRequest }; 
            var result =_mediator.Send(socialsQuery);
            return Ok(result);  
        }
    }
}
