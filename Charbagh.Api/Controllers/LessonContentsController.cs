using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.LessonContents.Create;
using Charbagh.Application.LessonContents.GetById;
using Charbagh.Application.LessonContents.GetAll;\nusing Charbagh.Application.LessonContents.GetByLessonSession;
using Charbagh.Application.LessonContents.SetTeaching;
using Charbagh.Application.LessonContents.SetPractice;
using Charbagh.Application.LessonContents.SetPerformance;
using Charbagh.Application.LessonContents.SetReview;
using Charbagh.Application.LessonContents.Update;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonContentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LessonContentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateLessonContentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllLessonContentsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("session/{lessonSessionId:guid}")]
    public async Task<IActionResult> GetByLessonSession(Guid lessonSessionId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLessonContentsBySessionQuery(lessonSessionId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLessonContentByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/set-teaching")]
    public async Task<IActionResult> SetTeaching(Guid id  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetTeachingLessonContentTypeCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-practice")]
    public async Task<IActionResult> SetPractice(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetPracticeLessonContentTypeCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-performance")]
    public async Task<IActionResult> SetPerformance(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetPerformanceLessonContentTypeCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/set-review")]
    public async Task<IActionResult> SetReview(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetReviewLessonContentTypeCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateLessonContentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}