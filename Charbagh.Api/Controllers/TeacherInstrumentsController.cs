using Charbagh.Application.TeacherInstruments.Activate;
using Charbagh.Application.TeacherInstruments.Create;
using Charbagh.Application.TeacherInstruments.Deactivate;
using Charbagh.Application.TeacherInstruments.GetAll;
using Charbagh.Application.TeacherInstruments.GetById;
using Charbagh.Application.TeacherInstruments.GetByInstrumentId;
using Charbagh.Application.TeacherInstruments.GetByTeacherId;
using Charbagh.Application.TeacherInstruments.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public sealed class TeacherInstrumentsController : ControllerBase
{
    private readonly ISender _sender;

    public TeacherInstrumentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTeacherInstrumentCommand command,
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
            new GetTeacherInstrumentByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllTeacherInstrumentsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("teacher/{teacherId:guid}")]
    public async Task<IActionResult> GetByTeacherId(
        Guid teacherId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetTeacherInstrumentsByTeacherIdQuery(teacherId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("instrument/{instrumentId:guid}")]
    public async Task<IActionResult> GetByInstrumentId(
        Guid instrumentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetTeacherInstrumentsByInstrumentIdQuery(instrumentId),
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTeacherInstrumentCommand command,
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
            new ActivateTeacherInstrumentCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeactivateTeacherInstrumentCommand(id),
            cancellationToken);

        return Ok(result);
    }
}