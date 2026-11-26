using MediatR;

namespace Charbagh.Application.InvoiceItems.Remove;

public sealed record RemoveInvoiceItemCommand(Guid Id) : IRequest;
