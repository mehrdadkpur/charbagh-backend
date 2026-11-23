using Charbagh.Application.RolePermissions.AssignToRole;
using Charbagh.Application.RolePermissions.Remove;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionsController : ControllerBase
{
    private readonly ISender _sender;

    public RolePermissionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Assign(
        AssignPermissionToRoleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{roleId:guid}/{permissionId:guid}")]
    public async Task<IActionResult> Remove(
        Guid roleId,
        Guid permissionId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RemovePermissionFromRoleCommand(
                roleId,
                permissionId),
            cancellationToken);

        return NoContent();
    }
}