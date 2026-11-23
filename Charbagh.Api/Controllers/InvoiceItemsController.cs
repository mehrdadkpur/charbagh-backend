using Charbagh.Application.InvoiceItems.Create;
using Charbagh.Application.InvoiceItems.GetAll;
using Charbagh.Application.InvoiceItems.GetById;
using Charbagh.Application.InvoiceItems.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InvoiceItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateInvoiceItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

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