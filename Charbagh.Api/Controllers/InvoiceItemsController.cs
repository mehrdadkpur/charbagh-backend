using Charbagh.Application.InvoiceItems.Create;
using Charbagh.Application.InvoiceItems.GetAll;
using Charbagh.Application.InvoiceItems.GetById;
using Charbagh.Application.InvoiceItems.Update;
using Charbagh.Application.InvoiceItems.Remove;
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.InvoiceItems.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InvoiceItemsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IInvoiceItemRepository _repository;

    public InvoiceItemsController(ISender sender, IInvoiceItemRepository repository)
    {
        _sender = sender;
        _repository = repository;
    }

    [HttpGet("invoice/{invoiceId:guid}")]
    public async Task<IActionResult> GetByInvoice(Guid invoiceId, CancellationToken cancellationToken)
    {
        var items = await _repository.GetByInvoiceIdAsync(invoiceId, cancellationToken);
        return Ok(items.Select(item => item.ToDto()).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateInvoiceItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllInvoiceItemsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceItemByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new RemoveInvoiceItemCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateInvoiceItemCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}