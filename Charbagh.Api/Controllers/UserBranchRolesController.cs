using Charbagh.Api.Authorization;
using Charbagh.Application.UserBranchRoles.Activate;
using Charbagh.Application.UserBranchRoles.Assign;
using Charbagh.Application.UserBranchRoles.GetAll;
using Charbagh.Application.UserBranchRoles.GetById;
using Charbagh.Application.UserBranchRoles.GetByRoleId;
using Charbagh.Application.UserBranchRoles.GetByUserId;
using Charbagh.Application.UserBranchRoles.RemoveRoleFromUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserBranchRolesController : ControllerBase
{
    private readonly ISender _sender;

    public UserBranchRolesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Assign( AssignCommand command,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Assign),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserBranchRoleByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllUserBranchRolesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserBranchRolesByUserIdQuery(userId),cancellationToken);

        return Ok(result);
    }

    [HttpGet("role/{roleId:guid}")]
    public async Task<IActionResult> GetByRoleId(Guid roleId,CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new GetUserBranchRolesByRoleIdQuery(roleId),cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new ActivateUserBranchRoleCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/remove")]
    [HasPermission("UserBranchRoles", PermissionAction.Delete)]
    public async Task<IActionResult> RemoveRoleFromUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RemoveRoleFromUserCommand(id), cancellationToken);

        return NoContent();
    }
}