using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.StudentParents.Create;
using Charbagh.Application.StudentParents.GetByParentId;
using Charbagh.Application.StudentParents.UpdateRelationship;
using Charbagh.Application.StudentParents.GetById;
using Charbagh.Application.StudentParents.GetByStudentId;
using Charbagh.Application.StudentParents.Remove;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentParentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentParentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateStudentParentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.StudentParents.GetById), new { id = result.Id }, result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentParentByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpGet("parent/{parentId:guid}")]
    public async Task<IActionResult> GetByParentId(Guid parentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentParentByParentIdQuery(parentId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudentId(Guid studentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentParentByStudentIdQuery(studentId), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRelationShip(Guid id,UpdateStudentParentRelationCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveStudentParent( Guid id,CancellationToken cancellationToken)
    {
        await _mediator.Send( new RemoveStudentParentCommand(id), cancellationToken);

        return NoContent();
    }
}