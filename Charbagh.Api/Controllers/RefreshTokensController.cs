using Charbagh.Application.RefreshTokens.Create;
using Charbagh.Application.RefreshTokens.GetAll;
using Charbagh.Application.RefreshTokens.GetById;
using Charbagh.Application.RefreshTokens.GetByIpAddress;
using Charbagh.Application.RefreshTokens.GetByUserId;
using Charbagh.Application.RefreshTokens.SetReplace;
using Charbagh.Application.RefreshTokens.SetRevoke;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RefreshTokensController : ControllerBase
{
    private readonly ISender _sender;

    public RefreshTokensController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateRefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRefreshTokenByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllRefreshTokensQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("ip-address/{ipAddress}")]
    public async Task<IActionResult> GetByIpAddress(
        string ipAddress,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRefreshTokensByIpAddressQuery(ipAddress),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRefreshTokensByUserIdQuery(userId),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/revoke")]
    public async Task<IActionResult> SetRevoke(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new SetRevokeRefreshTokenCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/replace")]
    public async Task<IActionResult> SetReplace(
        Guid id,
        SetReplaceRefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}

