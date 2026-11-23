using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.LessonAssignments.Create;
using Charbagh.Application.LessonAssignments.GetAll;
using Charbagh.Application.LessonAssignments.GetById;
using Charbagh.Application.LessonAssignments.Complete;
using Charbagh.Application.LessonAssignments.Suspend;
using Charbagh.Application.LessonAssignments.Resume;
using Charbagh.Application.LessonAssignments.GetByStudentId;
using Charbagh.Application.LessonAssignments.Update;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LessonAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateLessonAssignmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.LessonAssignments.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllLessonAssignmentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLessonAssignmentByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpGet("studentId/{id:guid}")]
    public async Task<IActionResult> GetByStudentId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLessonAssignmentByStudentIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id , DateOnly EndDate , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CompleteLessonAssignmentCommand(id , EndDate), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/suspend")]
    public async Task<IActionResult> Suspend(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SuspendLessonAssignmentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/resume")]
    public async Task<IActionResult> Resume(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ResumeLessonAssignmentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateLessonAssignmentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}