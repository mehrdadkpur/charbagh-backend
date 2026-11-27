using Charbagh.Api.Authorization;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.LoginHistories.GetAll;
using Charbagh.Application.LoginHistories.GetById;
using Charbagh.Application.LoginHistories.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Charbagh.Domain.Enums;

namespace Charbagh.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class LoginHistoriesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ICurrentUser _currentUser;

    public LoginHistoriesController(ISender sender, ICurrentUser currentUser)
    {
        _sender = sender;
        _currentUser = currentUser;
    }

    [HttpGet("{id:guid}")]
    [HasPermission("LoginHistories", PermissionAction.View)]
    public async Task<IActionResult> GetById( Guid id,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetLoginHistoryByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [HasPermission("LoginHistories", PermissionAction.View)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllLoginHistoriesQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMine(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetLoginHistoriesByUserIdQuery(_currentUser.UserId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    [HasPermission("LoginHistories", PermissionAction.View)]
    public async Task<IActionResult> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetLoginHistoriesByUserIdQuery(userId),
            cancellationToken);

        return Ok(result);
    }
}