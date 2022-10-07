using Application.Features.OperationClaim.Commands.CreateOperationClaim;
using Application.Features.OperationClaim.Commands.DeleteOperationClaim;
using Application.Features.OperationClaim.Commands.UpdateOperationClaim;
using Application.Features.OperationClaim.Queries;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            var result = await Mediator.Send(createOperationClaimCommand);

            return Created("", result);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            var result = await Mediator.Send(deleteOperationClaimCommand);

            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            var result = await Mediator.Send(updateOperationClaimCommand);

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetListOperationClaimQuery()
            {
                PageRequest = pageRequest
            };
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
