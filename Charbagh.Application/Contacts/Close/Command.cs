using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.Close
{
    public sealed record CloseContactCommand(Guid Id) : IRequest<ContactDto>;
}
