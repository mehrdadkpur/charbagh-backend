
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.GetAll
{
    public sealed record GetallBookItemsQuery : IRequest<IReadOnlyList< BookItemDto>>;
}
