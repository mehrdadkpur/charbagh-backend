using Charbagh.Application.SecurityAudits.GetAll;
using Charbagh.Application.SecurityAudits.GetByAction;
using Charbagh.Application.SecurityAudits.GetById;
using Charbagh.Application.SecurityAudits.GetByUserId;
using Charbagh.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SecurityAuditsController : ControllerBase
{
    private readonly ISender _sender;

    public SecurityAuditsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetSecurityAuditByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllSecurityAuditsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetSecurityAuditsByUserIdQuery(userId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("action/{action}")]
    public async Task<IActionResult> GetByAction(
        SecurityAuditAction action,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetSecurityAuditsByActionQuery(action),
            cancellationToken);

        return Ok(result);
    }
}