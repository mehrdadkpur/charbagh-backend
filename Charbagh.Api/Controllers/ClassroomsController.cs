using Charbagh.Application.Classrooms.Activate;
using Charbagh.Application.Classrooms.Create;
using Charbagh.Application.Classrooms.Deactivate;
using Charbagh.Application.Classrooms.GetAll;
using Charbagh.Application.Classrooms.GetByBranchId;
using Charbagh.Application.Classrooms.GetById;
using Charbagh.Application.Classrooms.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClassroomsController : ControllerBase
{
    private readonly ISender _sender;

    public ClassroomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateClassroomCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetClassroomByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllClassroomsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetByBranchId(
        Guid branchId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetClassroomsByBranchIdQuery(branchId),
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateClassroomCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ActivateClassroomCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeactivateClassroomCommand(id),
            cancellationToken);

        return Ok(result);
    }
}