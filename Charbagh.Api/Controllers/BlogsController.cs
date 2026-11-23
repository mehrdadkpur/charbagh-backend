using Charbagh.Application.Blogs.Create;
using Charbagh.Application.Blogs.GetAll;
using Charbagh.Application.Blogs.GetById;
using Charbagh.Application.Blogs.SetArchive;
using Charbagh.Application.Blogs.SetPublish;
using Charbagh.Application.Blogs.SetRestore;
using Charbagh.Application.Blogs.SetUnpublish;
using Charbagh.Application.Blogs.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsController : ControllerBase
{
    private readonly ISender _sender;

    public BlogsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateBlogCommand command,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBlogByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllBlogsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update( Guid id, UpdateBlogCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> SetArchive( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new SetArchiveBlogCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/publish")]
    public async Task<IActionResult> SetPublish( Guid id,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetPublishBlogCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/restore")]
    public async Task<IActionResult> SetRestore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetRestoreBlogCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/unpublish")]
    public async Task<IActionResult> SetUnpublish(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetUnpublishBlogCommand(id), cancellationToken);

        return Ok(result);
    }
}