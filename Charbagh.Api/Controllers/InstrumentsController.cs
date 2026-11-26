using Charbagh.Application.Instruments.Activate;
using Charbagh.Application.Instruments.Create;
using Charbagh.Application.Instruments.Deactivate;
using Charbagh.Application.Instruments.GetAll;
using Charbagh.Application.Instruments.GetById;
using Charbagh.Application.Instruments.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InstrumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstrumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateInstrumentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllInstrumentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInstrumentByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new ActivateInstrumentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateInstrumentCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateInstrumentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}