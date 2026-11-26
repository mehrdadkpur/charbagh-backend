using Charbagh.Api.Authorization;
using Charbagh.Application.Galleries.Archive;
using Charbagh.Application.Galleries.Create;
using Charbagh.Application.Galleries.GetAll;
using Charbagh.Application.Galleries.GetById;
using Charbagh.Application.Galleries.GetByStatus;
using Charbagh.Application.Galleries.GetByTitle;
using Charbagh.Application.Galleries.Restore;
using Charbagh.Application.Galleries.SetPublish;
using Charbagh.Application.Galleries.SetUnpublish;
using Charbagh.Application.Galleries.Update;
using Charbagh.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GalleriesController : ControllerBase
{
    private readonly ISender _sender;

    public GalleriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission("Galleries", PermissionAction.Create)]
    public async Task<IActionResult> Create(CreateGalleryCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("published")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublished(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllGalleriesQuery(), cancellationToken);
        return Ok(result.Where(gallery => gallery.Status == GalleryStatus.Published));
    }

    [HttpGet("published/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublishedById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetGalleryByIdQuery(id), cancellationToken);
        if (result.Status != GalleryStatus.Published)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Galleries", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetGalleryByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Galleries", PermissionAction.View)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllGalleriesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("status/{status}")]
    [HasPermission("Galleries", PermissionAction.View)]
    public async Task<IActionResult> GetByStatus(GalleryStatus status, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetByStatusGalleriesQuery(status), cancellationToken);
        return Ok(result);
    }

    [HttpGet("title/{title}")]
    [HasPermission("Galleries", PermissionAction.View)]
    public async Task<IActionResult> GetByTitle(string title, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetByTitleGalleriesQuery(title), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Galleries", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateGalleryCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/archive")]
    [HasPermission("Galleries", PermissionAction.Delete)]
    public async Task<IActionResult> Archive(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new ArchiveGalleryCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/publish")]
    [HasPermission("Galleries", PermissionAction.Approve)]
    public async Task<IActionResult> SetPublish(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetPublishGalleryCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/restore")]
    [HasPermission("Galleries", PermissionAction.Update)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RestoreGalleryCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/unpublish")]
    [HasPermission("Galleries", PermissionAction.Approve)]
    public async Task<IActionResult> SetUnpublish(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new SetUnpublishGalleryCommand(id), cancellationToken);
        return Ok(result);
    }
}
