using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Authentication.CreateUser;
using Charbagh.Application.Authentication.Login;
using Charbagh.Application.Authentication.Logout;
using Charbagh.Application.Authentication.RefreshToken;
using Charbagh.Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers.Identity;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ICurrentUser _currentUser;

    public AuthenticationController(ISender sender , ICurrentUser currentUser)
    {
        _sender = sender;
        _currentUser = currentUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult<CreateUserResponse>> Register( [FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login( [FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers["User-Agent"].ToString();

        var result = await _sender.Send(
            command with { IpAddress = ipAddress, UserAgent = userAgent },
            cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = _currentUser.UserId,
            Mobile = _currentUser.Mobile,
            FullName = _currentUser.FullName,
            IsAuthenticated = _currentUser.IsAuthenticated
        });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(
    [FromBody] RefreshTokenCommand command,
    CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            command,
            cancellationToken);

        return Ok(response);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(
    [FromBody] LogoutCommand command,
    CancellationToken cancellationToken)
    {
        await _sender.Send(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpGet("current-branch")]
    public IActionResult GetCurrentBranch(
    [FromServices] ICurrentBranch currentBranch)
    {
        return Ok(new
        {
            currentBranch.BranchId,
            currentBranch.HasBranch
        });
    }
}