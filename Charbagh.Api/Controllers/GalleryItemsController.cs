using Charbagh.Api.Authorization;
using Charbagh.Application.GalleryItems.ChangeSortOrder;
using Charbagh.Application.GalleryItems.Create;
using Charbagh.Application.GalleryItems.Delete;
using Charbagh.Application.GalleryItems.GetAll;
using Charbagh.Application.GalleryItems.GetById;
using Charbagh.Application.GalleryItems.Update;
using Charbagh.Domain.Enums;
using MediatR;
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
    public async Task<IActionResult> Create(
        CreateGalleryItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
    public async Task<IActionResult> Update(
        Guid id,
        UpdateGalleryItemCommand command,
        CancellationToken cancellationToken)
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
        var result = await _sender.Send(
            new ChangeSortOrderGalleryItemCommand(id, sortOrder),
            cancellationToken);
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
