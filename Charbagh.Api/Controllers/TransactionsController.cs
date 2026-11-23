using Charbagh.Application.Transactions.Cancel;
using Charbagh.Application.Transactions.Create;
using Charbagh.Application.Transactions.GetAll;
using Charbagh.Application.Transactions.GetById;
using Charbagh.Application.Transactions.GetByPayment;
using Charbagh.Application.Transactions.MarkAsFailed;
using Charbagh.Application.Transactions.MarkAsSuccessful;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionsController : ControllerBase
{
    private readonly ISender _sender;

    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTransactionCommand command,
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
            new GetTransactionByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllTransactionsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("payment/{paymentId:guid}")]
    public async Task<IActionResult> GetByPayment(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetTransactionsByPaymentQuery(paymentId),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/successful")]
    public async Task<IActionResult> MarkSuccessful(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new MarkTransactionSuccessfulCommand(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/failed")]
    public async Task<IActionResult> MarkFailed(
        Guid id,
        [FromBody] string? description,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new MarkTransactionFailedCommand(
                id,
                description),
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid id,
        [FromBody] string? description,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CancelTransactionCommand(
                id,
                description),
            cancellationToken);

        return Ok(result);
    }
}