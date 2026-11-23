using Charbagh.Api.Authorization;
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using Charbagh.Application.Roles.Activate;
using Charbagh.Application.Roles.AssignPermission;
using Charbagh.Application.Roles.AssignUser;
using Charbagh.Application.Roles.Create;
using Charbagh.Application.Roles.Deactivate;
using Charbagh.Application.Roles.GetAll;
using Charbagh.Application.Roles.GetById;
using Charbagh.Application.Roles.GetByIsActive;
using Charbagh.Application.Roles.GetByIsSystem;
using Charbagh.Application.Roles.GetByName;
using Charbagh.Application.Roles.RemovePermission;
using Charbagh.Application.Roles.RemoveUser;
using Charbagh.Application.Roles.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPermissionRepository _permissionRepository;

    public RolesController(
        ISender sender,
        IPermissionRepository permissionRepository)
    {
        _sender = sender;
        _permissionRepository = permissionRepository;
    }

    [HttpPost]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> Create(
        CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRoleByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllRolesQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}/available-users")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetAvailableUsers(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAvailableUsersForRoleQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Roles", PermissionAction.Update)]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("system/{isSystem}")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetByIsSystem(
        bool isSystem,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRolesByIsSystemQuery(isSystem),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("name/{name}")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetByName(
        string name,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRolesByNameQuery(name),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("active/{isActive}")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetByIsActive(
        bool isActive,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRolesByIsActiveQuery(isActive),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Roles", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ActivateRoleCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    [HasPermission("Roles", PermissionAction.Manage)]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeactivateRoleCommand(id),
            cancellationToken);

        return Ok(result);
    }

    public sealed record AssignUserRequest(Guid UserBranchId);

    [HttpPost("{id:guid}/users")]
    [HasPermission("Roles", PermissionAction.Create)]
    public async Task<IActionResult> AssignUser(
        [FromRoute] Guid id,
        [FromBody] AssignUserRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new AssignUserToRoleCommand(id, request.UserBranchId),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}/users/{userBranchId:guid}")]
    [HasPermission("Roles", PermissionAction.Manage)]
    public async Task<IActionResult> RemoveUser(
        Guid id,
        Guid userBranchId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RemoveUserFromRoleCommand(id, userBranchId),
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/permissions/{permissionId:guid}")]
    [HasPermission("Roles", PermissionAction.Manage)]
    public async Task<IActionResult> AddPermission(
        Guid id,
        Guid permissionId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new AddPermissionToRoleCommand(id, permissionId),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}/permissions/{permissionId:guid}")]
    [HasPermission("Roles", PermissionAction.Manage)]
    public async Task<IActionResult> RemovePermission(
        Guid id,
        Guid permissionId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RemovePermissionFromRoleCommand(id, permissionId),
            cancellationToken);

        return NoContent();
    }

    [HttpGet("permissions/available")]
    [HasPermission("Roles", PermissionAction.View)]
    public async Task<IActionResult> GetAvailablePermissions(
        CancellationToken cancellationToken)
    {
        var result = await _permissionRepository.GetAllAsync(
            cancellationToken);

        return Ok(result.Select(x => new PermissionDto(
            x.Id,
            x.Module,
            x.Name,
            x.Action,
            x.DisplayName,
            x.Description,
            x.IsSystem)));
    }
}

public sealed record AssignUserRequest(Guid UserBranchId);