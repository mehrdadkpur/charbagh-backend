using Charbagh.Application.Invoices.AddInvoiceItem;
using Charbagh.Application.Invoices.CancelInvoice;
using Charbagh.Application.Invoices.ConfirmInvoice;
using Charbagh.Application.Invoices.Create;
using Charbagh.Application.Invoices.GetAll;
using Charbagh.Application.Invoices.GetById;
using Charbagh.Application.Invoices.GetByStudentId;
using Charbagh.Application.Invoices.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Invoices.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllInvoicesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudentId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllInvoicesByStudentIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> CancelInvoice(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CancelInvoiceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/confirm")]
    public async Task<IActionResult> ConfirmInvoice(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ConfirmInvoiceCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateInvoiceCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}