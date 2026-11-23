
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.Deactivate
{
    public sealed record DeactivateBookItemCommand(Guid Id) : IRequest<BookItemDto>;
}
