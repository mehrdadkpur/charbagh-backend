
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.Activate
{
    public sealed record ActivateBookCommand(Guid Id): IRequest<BookDto>;
}
