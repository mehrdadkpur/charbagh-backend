using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.MarkAsReplied
{
    public sealed record MarkAsRepliedContactCommand(Guid Id) : IRequest<ContactDto>;
}
