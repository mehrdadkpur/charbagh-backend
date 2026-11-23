using Charbagh.Application.Attendances.Create;
using Charbagh.Application.Attendances.GetAll;
using Charbagh.Application.Attendances.GetById;
using Charbagh.Application.Attendances.SetAbsent;
using Charbagh.Application.Attendances.SetExcuse;
using Charbagh.Application.Attendances.SetLate;
using Charbagh.Application.Attendances.SetPresent;
using Charbagh.Application.Attendances.Update;
using Charbagh.Application.ServicePrices.Create;
using Charbagh.Application.ServicePrices.GetAll;
using Charbagh.Application.ServicePrices.GetById;
using Charbagh.Application.ServicePrices.SetEffectiveTo;
using Charbagh.Application.ServicePrices.UpdatePrice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ServicePricesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicePricesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateServicePriceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction( nameof(Application.ServicePrices.GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll ( CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new GetAllServicePricesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetServicPriceByIdQuery(id), cancellationToken);
            return Ok(result);
        }

    [HttpPatch("{id:guid}/set-effectiveto")]
    public async Task<IActionResult> SetEffectiveTo(Guid id , DateOnly effectiveTo  , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetEffectiveToServicPriceCommand(id ,effectiveTo), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePirce(Guid id,UpdatePriceServicePriceCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var result =await _mediator.Send( command, cancellationToken);
        return Ok(result);
    }
}