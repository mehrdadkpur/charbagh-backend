
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.Activate
{
    public sealed record ActivateBookItemCommand(Guid Id) : IRequest<BookItemDto>;
}
