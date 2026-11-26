using Charbagh.Application.Attendances.Create;
using Charbagh.Application.EducationServices.Activate;
using Charbagh.Application.EducationServices.Create;
using Charbagh.Application.EducationServices.Deactivate;
using Charbagh.Application.EducationServices.GetAll;
using Charbagh.Application.EducationServices.GetById;
using Charbagh.Application.EducationServices.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EducationServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EducationServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEducationServiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllEducationServicesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetEducationServiceByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateEducationServiceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateEducationServiceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEducationServiceCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}