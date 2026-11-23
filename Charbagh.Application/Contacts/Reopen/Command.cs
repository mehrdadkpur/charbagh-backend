using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.Reopen
{
    public sealed record ReopenContactCommand(Guid Id) : IRequest<ContactDto>;
}
