
using Charbagh.Application.InstrumentCategories.Activate;
using Charbagh.Application.InstrumentCategories.Create;
using Charbagh.Application.InstrumentCategories.Deactivate;
using Charbagh.Application.InstrumentCategories.GetAll;
using Charbagh.Application.InstrumentCategories.GetById;
using Charbagh.Application.InstrumentCategories.Update;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InstrumentCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstrumentCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateInstrumentCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
          nameof(GetById),
          new { id = result.Id },
          result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send( new GetInstrumentCategoryByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    { 
        var result = await _mediator.Send(new GetAllInstrumentCategoriesQuery(),cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate (Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateInstrumentCategoryCommand(id),cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateInstrumentCategoryCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateInstrumentCategoryCommand command, CancellationToken cancellationToken)
    {
        if(id != command.Id)
        {
            return BadRequest();
        }
        var result = await _mediator.Send(command , cancellationToken);
        return Ok(result);
    }
}