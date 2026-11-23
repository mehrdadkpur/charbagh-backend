
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.GetById
{
    public sealed record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;
}
