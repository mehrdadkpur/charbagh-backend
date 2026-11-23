using Charbagh.Application.UserBranches.Activate;
using Charbagh.Application.UserBranches.Assign;
using Charbagh.Application.UserBranches.GetAll;
using Charbagh.Application.UserBranches.GetById;
using Charbagh.Application.UserBranches.GetByUserId;
using Charbagh.Application.UserBranches.RemoveUserFromBranch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserBranchesController : ControllerBase
{
    private readonly ISender _sender;

    public UserBranchesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Assign(AssignUserToBranchCommand command,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Assign),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserBranchByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllUserBranchesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserBranchesByUserIdQuery(userId),cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new ActivateUserBranchCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/remove")]
    public async Task<IActionResult> RemoveuserFromBranch(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RemoveUserFromBranchCommand(id), cancellationToken);

        return NoContent();
    }
}