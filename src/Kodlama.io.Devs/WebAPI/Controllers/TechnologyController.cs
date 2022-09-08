using Application.Features.Technology.Queires.GetById;
using Application.Features.Technology.Queires.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery technologyQuery = new() { PageRequest = pageRequest };
            var result = await Mediator.Send(technologyQuery);
            return Ok(result);  
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery )
        {
           var result = await Mediator.Send(getByIdTechnologyQuery);
           return Ok(result);
        }
    }
}
