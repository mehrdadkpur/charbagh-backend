using Charbagh.Api.Authorization;
using Charbagh.Application.Comments.Create;
using Charbagh.Application.Comments.GetAll;
using Charbagh.Application.Comments.GetByBlogId;
using Charbagh.Application.Comments.GetById;
using Charbagh.Application.Comments.GetByUserId;
using Charbagh.Application.Comments.SetApprove;
using Charbagh.Application.Comments.SetPending;
using Charbagh.Application.Comments.SetReject;
using Charbagh.Application.Comments.Update;
using Charbagh.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CommentsController : ControllerBase
{
    private readonly ISender _sender;

    public CommentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission("Comments", PermissionAction.Create)]
    public async Task<IActionResult> Create(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Comments", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCommentByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Comments", PermissionAction.View)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllCommentsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    [HasPermission("Comments", PermissionAction.View)]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCommentsByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("blog/{blogId:guid}")]
    [HasPermission("Comments", PermissionAction.View)]
    public async Task<IActionResult> GetByBlogId(Guid blogId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCommentsByBlogIdQuery(blogId), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Comments", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/approve")]
    [HasPermission("Comments", PermissionAction.Approve)]
    public async Task<IActionResult> SetApprove(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetApproveCommentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/pending")]
    [HasPermission("Comments", PermissionAction.Approve)]
    public async Task<IActionResult> SetPending(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetPendingCommentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/reject")]
    [HasPermission("Comments", PermissionAction.Approve)]
    public async Task<IActionResult> SetReject(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetRejectCommentCommand(id), cancellationToken);
        return Ok(result);
    }
}
