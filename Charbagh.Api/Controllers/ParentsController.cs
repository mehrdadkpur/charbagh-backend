using Charbagh.Api.Authorization;
using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Charbagh.Application.Parents.Activate;
using Charbagh.Application.Parents.Block;
using Charbagh.Application.Parents.Create;
using Charbagh.Application.Parents.GetAll;
using Charbagh.Application.Parents.GetById;
using Charbagh.Application.Parents.Suspend;
using Charbagh.Application.Parents.Unblock;
using Charbagh.Application.Teachers.Activate;
using Charbagh.Application.Teachers.Block;
using Charbagh.Application.Teachers.Create;
using Charbagh.Application.Teachers.GetAll;
using Charbagh.Application.Teachers.GetById;
using Charbagh.Application.Teachers.Suspend;
using Charbagh.Application.Teachers.Unblock;
using Charbagh.Application.Teachers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ParentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [HasPermission("Parents", PermissionAction.Create)]
    public async Task<IActionResult> Create( CreateParentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Parents.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [HasPermission("Parents", PermissionAction.View)]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllParentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Parents", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetParentByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Parents", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateParentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/suspend")]
    [HasPermission("Parents", PermissionAction.Manage)]
    public async Task<IActionResult> Suspend(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SuspendParentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/block")]
    [HasPermission("Parents", PermissionAction.Manage)]
    public async Task<IActionResult> Block(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BlockParentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/unblock")]
    [HasPermission("Parents", PermissionAction.Manage)]
    public async Task<IActionResult> Unblock(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnblockParentCommand(id), cancellationToken);
        return Ok(result);
    }

}