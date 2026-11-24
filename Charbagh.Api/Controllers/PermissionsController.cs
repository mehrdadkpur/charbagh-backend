using Charbagh.Api.Authorization;
using Charbagh.Application.Permissions.Create;
using Charbagh.Application.Permissions.GetAll;
using Charbagh.Application.Permissions.GetById;
using Charbagh.Application.Permissions.GetByIsSystem;
using Charbagh.Application.Permissions.GetByModule;
using Charbagh.Application.Permissions.GetByName;
using Charbagh.Application.Permissions.GetCurrent;
using Charbagh.Application.Permissions.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PermissionsController : ControllerBase
{
    private readonly ISender _sender;

    public PermissionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission("Permissions", PermissionAction.Create)]
    public async Task<IActionResult> Create(CreatePermissionCommand command,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Permissions", PermissionAction.View)]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetPermissionByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Permissions",PermissionAction.View)]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllPermissionsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Permissions", PermissionAction.Update)]
    public async Task<IActionResult> Update( Guid id, UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("system/{isSystem}")]
    [HasPermission("Permissions", PermissionAction.View)]
    public async Task<IActionResult> GetByIsSystem(bool isSystem, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetPermissionsByIsSystemQuery(isSystem), cancellationToken);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    [HasPermission("Permissions", PermissionAction.View)]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetPermissionsByNameQuery(name), cancellationToken);
        return Ok(result);
    }

    [HttpGet("module/{module}")]
    [HasPermission("Permissions", PermissionAction.View)]
    public async Task<IActionResult> GetByModule(string module, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetPermissionsByModuleQuery(module), cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent( CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new GetCurrentPermissionsQuery(),cancellationToken);

        if (result is null)
        {
            return BadRequest(new
            {
                message = "ابتدا شعبه فعال را انتخاب کنید."
            });
        }

        return Ok(result);
    }

}