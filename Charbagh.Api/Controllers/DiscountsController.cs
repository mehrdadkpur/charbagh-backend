using Charbagh.Application.Discounts.Common;
using Charbagh.Application.Discounts.Create;
using Charbagh.Application.Discounts.GetAll;
using Charbagh.Application.Discounts.GetById;
using Charbagh.Application.Discounts.GetByInvoice;
using Charbagh.Application.Discounts.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers.Finance;

[ApiController]
[Route("api/discounts")]
public sealed class DiscountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<ActionResult<DiscountDto>> Create(
        CreateDiscountCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            request,
            cancellationToken);

        return Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DiscountDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetDiscountByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DiscountDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAllDiscountsQuery(),
            cancellationToken);

        return Ok(result);
    }


    [HttpGet("invoice/{invoiceId:guid}")]
    public async Task<ActionResult<DiscountDto?>> GetByInvoice(
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetDiscountByInvoiceQuery(invoiceId),
            cancellationToken);

        return Ok(result);
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DiscountDto>> Update(
        Guid id,
        UpdateDiscountCommand request,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest(
                "Route id and request id do not match.");
        }

        var result = await _mediator.Send(
            request,
            cancellationToken);

        return Ok(result);
    }
}