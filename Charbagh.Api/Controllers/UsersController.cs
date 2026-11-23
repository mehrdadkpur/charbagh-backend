using Charbagh.Api.Authorization;
using Charbagh.Application.Authentication.CreateUser;
using Charbagh.Application.Users.Activate;
using Charbagh.Application.Users.Block;
using Charbagh.Application.Users.Create;
using Charbagh.Application.Users.GetAll;
using Charbagh.Application.Users.GetById;
using Charbagh.Application.Users.GetByNationalCode;
using Charbagh.Application.Users.Suspend;
using Charbagh.Application.Users.Unblock;
using Charbagh.Application.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [HasPermission("Users", PermissionAction.Create)]
    public async Task<ActionResult<CreateUserResponse>> Create(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Users", PermissionAction.View)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send( new GetAllUsersQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{nationalCode}")]
    [HasPermission("Users", PermissionAction.View)]
    public async Task<IActionResult> GetByNationalCode(string nationalCode, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUserByNationalCodeQuery(nationalCode),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Users", PermissionAction.View)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Users", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ActivateUserCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/suspend")]
    [HasPermission("Users", PermissionAction.Manage)]
    public async Task<IActionResult> Suspend(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new SuspendUserCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/block")]
    [HasPermission("Users", PermissionAction.Manage)]
    public async Task<IActionResult> Block(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new BlockUserCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/unblock")]
    [HasPermission("Users", PermissionAction.Manage)]
    public async Task<IActionResult> Unblock(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UnblockUserCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Users", PermissionAction.Update)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}