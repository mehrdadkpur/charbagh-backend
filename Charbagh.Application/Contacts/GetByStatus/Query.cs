using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetByStatus
{
    public sealed record GetContactsByStatusQuery(ContactStatus Status) : IRequest<IReadOnlyList<ContactDto>>;
}
