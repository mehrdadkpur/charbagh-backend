using Charbagh.Application.BookItems.Activate;
using Charbagh.Application.BookItems.Deactivate;
using Charbagh.Application.BookItems.Update;
using Charbagh.Application.TeachingPlans.Activate;
using Charbagh.Application.TeachingPlans.Cancel;
using Charbagh.Application.TeachingPlans.Complete;
using Charbagh.Application.TeachingPlans.Create;
using Charbagh.Application.TeachingPlans.GetAll;
using Charbagh.Application.TeachingPlans.GetById;
using Charbagh.Application.TeachingPlans.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TeachingPlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeachingPlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateTeachingPlanCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.TeachingPlans.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllTeachingPlansQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTeachingPlanByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new ActivateTeachingPlanCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, DateOnly endDate , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CancelTeachingPlanCommand(id , endDate), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, DateOnly endDate, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CompleteTeachingPlanCommand(id, endDate), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTeachingPlanCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}