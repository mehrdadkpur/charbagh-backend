using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.LessonSessions.Create;
using Charbagh.Application.LessonSessions.GetById;
using Charbagh.Application.LessonSessions.GetAll;
using Charbagh.Application.LessonSessions.GetByAssignment;
using Charbagh.Application.LessonSessions.Update;
using Charbagh.Application.LessonSessions.Complete;
using Charbagh.Application.LessonSessions.Cancel;
using Charbagh.Application.LessonSessions.StudentAbsent;
using Charbagh.Application.LessonSessions.TeacherAbsent;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonSessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LessonSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateLessonSessionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllLessonSessionsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("assignment/{lessonAssignmentId:guid}")]
    public async Task<IActionResult> GetByAssignment(Guid lessonAssignmentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLessonSessionsByAssignmentQuery(lessonAssignmentId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLessonSessionByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetTeachingLessonSessionCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CancelLessonSessionCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/student-absent")]
    public async Task<IActionResult> StudentAbsent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StudentAbsentLessonSessionCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/teacher-absent")]
    public async Task<IActionResult> TeacherAbsent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TeacherAbsentLessonSessionCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateLessonSessionCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}