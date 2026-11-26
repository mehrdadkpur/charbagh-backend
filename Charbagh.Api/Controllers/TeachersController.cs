using Charbagh.Api.Authorization;
using Charbagh.Application.Teachers.Activate;
using Charbagh.Application.Teachers.Block;
using Charbagh.Application.Teachers.Create;
using Charbagh.Application.Teachers.GetAll;
using Charbagh.Application.Teachers.GetById;
using Charbagh.Application.Teachers.Suspend;
using Charbagh.Application.Teachers.Unblock;
using Charbagh.Application.Teachers.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TeachersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeachersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [HasPermission("Teachers", PermissionAction.Create)]
    public async Task<IActionResult> Create( [FromBody] CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet]
    [HasPermission("Teachers", PermissionAction.View)]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllTeachersQuery(), cancellationToken);

        return Ok(result);
    }

    // Public website endpoints expose only active teacher profile fields.
    [HttpGet("public")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicTeachers(CancellationToken cancellationToken)
    {
        var teachers = await _mediator.Send(new GetAllTeachersQuery(), cancellationToken);

        return Ok(teachers
            .Where(teacher => teacher.Status == TeacherStatus.Active)
            .Select(teacher => new
            {
                teacher.Id,
                teacher.FirstName,
                teacher.LastName,
                teacher.ProfileImageUrl,
                teacher.Education,
                teacher.ExperienceYears,
                teacher.Biography,
                teacher.Status
            }));
    }

    [HttpGet("public/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicTeacher(Guid id, CancellationToken cancellationToken)
    {
        var teacher = await _mediator.Send(new GetTeacherByIdQuery(id), cancellationToken);

        if (teacher.Status != TeacherStatus.Active)
            return NotFound();

        return Ok(new
        {
            teacher.Id,
            teacher.FirstName,
            teacher.LastName,
            teacher.ProfileImageUrl,
            teacher.Education,
            teacher.ExperienceYears,
            teacher.Biography,
            teacher.Status
        });
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Teachers", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTeacherByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Teachers", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateTeacherCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/suspend")]
    [HasPermission("Teachers", PermissionAction.Manage)]
    public async Task<IActionResult> Suspend(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SuspendTeacherCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/block")]
    [HasPermission("Teachers", PermissionAction.Manage)]
    public async Task<IActionResult> Block(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BlockTeacherCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/unblock")]
    [HasPermission("Teachers", PermissionAction.Manage)]
    public async Task<IActionResult> Unblock(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnblockTeacherCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Teachers", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id,UpdateTeacherCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}