using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.Attendances.Create;
using Charbagh.Application.Attendances.GetAll;
using Charbagh.Application.Attendances.GetById;
using Charbagh.Application.Attendances.GetByLessonSession;
using Charbagh.Application.Attendances.SetPresent;
using Charbagh.Application.Attendances.SetAbsent;
using Charbagh.Application.Attendances.SetLate;
using Charbagh.Application.Attendances.SetExcuse;
using Charbagh.Application.Attendances.Update;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AttendancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttendancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateAttendanceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllAttendancesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("session/{lessonSessionId:guid}")]
    public async Task<IActionResult> GetByLessonSession(Guid lessonSessionId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAttendanceByLessonSessionQuery(lessonSessionId), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAttendanceByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/set-present")]
    public async Task<IActionResult> SetPresent(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetPresentAttendanceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-absent")]
    public async Task<IActionResult> SetAbsent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetAbsentAttendanceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-late")]
    public async Task<IActionResult> SetLate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetLateAttendanceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-excuse")]
    public async Task<IActionResult> SetExcuse(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetExcuseAttendanceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateAttendanceCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}