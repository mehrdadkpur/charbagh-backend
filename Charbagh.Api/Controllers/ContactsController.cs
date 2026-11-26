using Charbagh.Application.Contacts.Close;
using Charbagh.Application.Contacts.Create;
using Charbagh.Application.Contacts.GetAll;
using Charbagh.Application.Contacts.GetById;
using Charbagh.Application.Contacts.GetbyMobile;
using Charbagh.Application.Contacts.GetByStatus;
using Charbagh.Application.Contacts.MarkAsReplied;
using Charbagh.Application.Contacts.Reopen;
using Charbagh.Application.Contacts.StartProcessing;
using MediatR;
using Charbagh.Api.Authorization;
using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly ISender _sender;

    public ContactsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
        CreateContactCommand command,
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
    [HasPermission("Contacts", PermissionAction.View)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetContactByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [HasPermission("Contacts", PermissionAction.View)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllContactsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("mobile/{mobile}")]
    [HasPermission("Contacts", PermissionAction.View)]
    public async Task<IActionResult> GetByMobile(
        string mobile,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetContactsByMobileQuery(mobile),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("status/{status}")]
    [HasPermission("Contacts", PermissionAction.View)]
    public async Task<IActionResult> GetByStatus(
        ContactStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetContactsByStatusQuery(status),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/processing")]
    [HasPermission("Contacts", PermissionAction.Manage)]
    public async Task<IActionResult> StartProcessing(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new StartProcessingContactCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/reply")]
    [HasPermission("Contacts", PermissionAction.Manage)]
    public async Task<IActionResult> MarkAsReplied(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new MarkAsRepliedContactCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/close")]
    [HasPermission("Contacts", PermissionAction.Manage)]
    public async Task<IActionResult> Close(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CloseContactCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/reopen")]
    [HasPermission("Contacts", PermissionAction.Manage)]
    public async Task<IActionResult> Reopen(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ReopenContactCommand(id),
            cancellationToken);

        return Ok(result);
    }
}