using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.Categories.Create;
using Charbagh.Application.Categories.GetAll;
using Charbagh.Application.Categories.GetById;
using Charbagh.Application.Categories.Activate;
using Charbagh.Application.Categories.Deactivate;
using Charbagh.Application.Categories.Update;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Categories.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateCategoryCommand(id), cancellationToken);
        return Ok(result);
    }


    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateCategoryCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}