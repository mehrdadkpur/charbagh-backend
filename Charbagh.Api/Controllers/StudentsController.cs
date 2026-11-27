using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.Students.Activate;
using Charbagh.Application.Students.Create;
using Charbagh.Application.Students.GetById;
using Charbagh.Application.Students.GetAll;
using Charbagh.Application.Students.Update;
using Charbagh.Application.Students.Block;
using Charbagh.Application.Students.Suspend;
using Charbagh.Application.Students.Unblock;
using Charbagh.Api.Authorization;
using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [HasPermission("Students", PermissionAction.Create)]
    public async Task<IActionResult> Create([FromBody] CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Students.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [HasPermission("Students", PermissionAction.View)]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllStudentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Students", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Students", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateStudentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/suspend")]
    [HasPermission("Students", PermissionAction.Manage)]
    public async Task<IActionResult> Suspend(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SuspendStudentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/block")]
    [HasPermission("Students", PermissionAction.Manage)]
    public async Task<IActionResult> Block(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BlockStudentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/unblock")]
    [HasPermission("Students", PermissionAction.Manage)]
    public async Task<IActionResult> Unblock(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnblockStudentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Students", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id,UpdateStudentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}