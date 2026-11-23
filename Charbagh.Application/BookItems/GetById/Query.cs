using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.GetById
{
    public sealed record GetBookItemByIdQuery(Guid Id) : IRequest<BookItemDto>;
}
