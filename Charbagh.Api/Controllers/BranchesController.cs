using Charbagh.Application.Branches.Activate;
using Charbagh.Application.Branches.Create;
using Charbagh.Application.Branches.Deactivate;
using Charbagh.Application.Branches.GetAll;
using Charbagh.Application.Branches.GetById;
using Charbagh.Application.Branches.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly ISender _sender;

    public BranchesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateBranchCommand command,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBranchByIdQuery(id),cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllBranchesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update( Guid id, UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate( Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send( new ActivateBranchCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate( Guid id,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeactivateBranchCommand(id), cancellationToken);

        return Ok(result);
    }
}