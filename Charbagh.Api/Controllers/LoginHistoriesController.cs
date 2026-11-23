using Charbagh.Application.LoginHistories.GetAll;
using Charbagh.Application.LoginHistories.GetById;
using Charbagh.Application.LoginHistories.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LoginHistoriesController : ControllerBase
{
    private readonly ISender _sender;

    public LoginHistoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetLoginHistoryByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllLoginHistoriesQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
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