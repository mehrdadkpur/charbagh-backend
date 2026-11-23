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
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllContactsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("mobile/{mobile}")]
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