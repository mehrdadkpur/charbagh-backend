using Charbagh.Api.Authorization;
using Charbagh.Application.Galleries.GetAll;
using Charbagh.Application.Galleries.GetById;
using Charbagh.Application.GalleryItems.ChangeSortOrder;
using Charbagh.Application.GalleryItems.Create;
using Charbagh.Application.GalleryItems.Delete;
using Charbagh.Application.GalleryItems.GetAll;
using Charbagh.Application.GalleryItems.GetById;
using Charbagh.Application.GalleryItems.Update;
using Charbagh.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GalleryItemsController : ControllerBase
{
    private readonly ISender _sender;

    public GalleryItemsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission("GalleryItems", PermissionAction.Create)]
    public async Task<IActionResult> Create(CreateGalleryItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublic(
        [FromQuery] GalleryMediaType? mediaType,
        CancellationToken cancellationToken)
    {
        var items = await _sender.Send(new GetAllGalleryItemsQuery(), cancellationToken);
        var galleries = await _sender.Send(new GetAllGalleriesQuery(), cancellationToken);
        var publishedGalleryIds = galleries
            .Where(gallery => gallery.Status == GalleryStatus.Published)
            .Select(gallery => gallery.Id)
            .ToHashSet();

        var result = items.Where(item => publishedGalleryIds.Contains(item.GalleryId));
        if (mediaType.HasValue)
            result = result.Where(item => item.MediaType == mediaType.Value);

        return Ok(result);
    }

    [HttpGet("public/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _sender.Send(new GetGalleryItemByIdQuery(id), cancellationToken);
        var gallery = await _sender.Send(new GetGalleryByIdQuery(item.GalleryId), cancellationToken);

        if (gallery.Status != GalleryStatus.Published)
            return NotFound();

        return Ok(item);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("GalleryItems", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetGalleryItemByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission("GalleryItems", PermissionAction.View)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllGalleryItemsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("GalleryItems", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateGalleryItemCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/change-sort-order")]
    [HasPermission("GalleryItems", PermissionAction.Update)]
    public async Task<IActionResult> ChangeSortOrder(
        Guid id,
        [FromQuery] int sortOrder,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new ChangeSortOrderGalleryItemCommand(id, sortOrder), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [HasPermission("GalleryItems", PermissionAction.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteGalleryItemCommand(id), cancellationToken);
        return NoContent();
    }
}
