
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.GetAll
{
    public sealed record GetAllBooksQuery:IRequest<IReadOnlyList<BookDto>>;
}
