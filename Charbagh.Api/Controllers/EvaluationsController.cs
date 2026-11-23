using Charbagh.Application.Evaluations.Create;
using Charbagh.Application.Evaluations.GetAll;
using Charbagh.Application.Evaluations.GetById;
using Charbagh.Application.Evaluations.SetComplete;
using Charbagh.Application.Evaluations.SetNotAssigned;
using Charbagh.Application.Evaluations.SetNotComplete;
using Charbagh.Application.Evaluations.SetPartiallyComplete;
using Charbagh.Application.Evaluations.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EvaluationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvaluationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEvaluationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Evaluations.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllEvaluationsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetEvaluationByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/set-notassigned")]
    public async Task<IActionResult> SetNotAssigned(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetNotAssignedPracticeCompletionStatusEvaluationCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-notcomplete")]
    public async Task<IActionResult> SetNotComplete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetNotCompletePracticeCompletionStatusEvaluationCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-partialycomplete")]
    public async Task<IActionResult> SetPartialyComplete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetPartiallyCompletePracticeCompletionStatusEvaluationCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-complete")]
    public async Task<IActionResult> SetComplete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetCompletePracticeCompletionStatusEvaluationCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEvaluationCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}