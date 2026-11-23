
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetAll
{
    public sealed record GetAllContactsQuery():IRequest<IReadOnlyList< ContactDto>>;
}
