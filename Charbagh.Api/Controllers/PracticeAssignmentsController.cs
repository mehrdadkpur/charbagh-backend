using Charbagh.Application.PracticeAssignments.Create;
using Charbagh.Application.PracticeAssignments.GetAll;
using Charbagh.Application.PracticeAssignments.GetById;
using Charbagh.Application.PracticeAssignments.SetHigh;
using Charbagh.Application.PracticeAssignments.SetLow;
using Charbagh.Application.PracticeAssignments.SetNormal;
using Charbagh.Application.PracticeAssignments.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PracticeAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PracticeAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreatePracticeAssignmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.PracticeAssignments.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllPracticeAssignmentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPracticeAssignmentByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/set-normal")]
    public async Task<IActionResult> SetNormal(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetNormalPriorityPracticeAssignmentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-high")]
    public async Task<IActionResult> SetAbsent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetHighPriorityPracticeAssignmentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-low")]
    public async Task<IActionResult> SetLate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetLowPriorityPracticeAssignmentCommand(id), cancellationToken);
        return Ok(result);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdatePracticeAssignmentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}