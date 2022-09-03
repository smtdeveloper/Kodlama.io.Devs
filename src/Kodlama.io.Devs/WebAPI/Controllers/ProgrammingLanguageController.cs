using Application.Features.ProgrammingLanguage.Commands;
using Application.Features.ProgrammingLanguage.Dto;
using Application.Features.ProgrammingLanguages.Commands;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammingLanguageController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
    {
        CreateProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguageCommand);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageQuery programmingLanguageQuery = new() { pageRequest = pageRequest };
        ProgrammingLanguageListModel result = await Mediator.Send(programmingLanguageQuery);
        return Ok(result);

    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery getProgrammingLanguageByIdQuery)
    {
        var result = await Mediator.Send(getProgrammingLanguageByIdQuery);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> SoftDelete([FromRoute] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(deleteProgrammingLanguageCommand);
        return Ok(result);  
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(updateProgrammingLanguageCommand);
        return Ok(result);
    }

}
