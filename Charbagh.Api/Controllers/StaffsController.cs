using MediatR;
using Microsoft.AspNetCore.Mvc;
using Charbagh.Application.Staffs.Create;
using Charbagh.Application.Staffs.GetById;
using Charbagh.Application.Staffs.Activate;
using Charbagh.Application.Staffs.Terminate;
using Charbagh.Application.Staffs.Update;
using Charbagh.Application.Staffs.GetAll;
using Charbagh.Application.Staffs.PutOnLeave;
using Charbagh.Api.Authorization;


namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StaffsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [HasPermission("Staffs", PermissionAction.Create)]
    public async Task<IActionResult> Create( CreateStaffCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.Staffs.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [HasPermission("Staffs", PermissionAction.View)]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllStaffsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Staffs" , PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStaffByIdQuery(id), cancellationToken);
            return Ok(result);

        }

    [HttpPatch("{id:guid}/activate")]
    [HasPermission("Staffs", PermissionAction.Manage)]
    public async Task<IActionResult> Activate(Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActivateStaffCommand(id), cancellationToken);
        return Ok(result);
    }


    [HttpPatch("{id:guid}/teminate")]
    [HasPermission("Staffs", PermissionAction.Manage)]
    public async Task<IActionResult> Teminate(Guid id ,DateOnly terminateDate, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TerminateStaffCommand(id,terminateDate), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/putOnLeave")]
    [HasPermission("Staffs", PermissionAction.Manage)]
    public async Task<IActionResult> PutOnLeave(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new PutOnLeaveStaffCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Staffs", PermissionAction.Update)]
    public async Task<IActionResult> Update(Guid id,UpdateStaffCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}