using Charbagh.Api.Authorization;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/authorization-test")]
public sealed class AuthorizationTestController : ControllerBase
{
    private readonly ICurrentBranch _currentBranch;

    public AuthorizationTestController(
        ICurrentBranch currentBranch)
    {
        _currentBranch = currentBranch;
    }

    [HttpGet("users")]
    [HasPermission("Users", PermissionAction.View)]
    public IActionResult ReadUsers(
        [FromHeader(Name = "X-Branch-Id")] Guid branchId)
    {
        return Ok(new
        {
            Message = "Permission granted.",
            BranchId = _currentBranch.BranchId
        });
    }

    [HttpGet("authenticated")]
    [Authorize]
    public IActionResult Authenticated()
    {
        return Ok(new
        {
            Message = "Authentication successful."
        });
    }
}