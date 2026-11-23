
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.Deactivate
{
    public sealed record DeactivateBookCommand(Guid Id): IRequest<BookDto>;
}
