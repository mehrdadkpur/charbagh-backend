using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetbyMobile
{
    public sealed record GetContactsByMobileQuery(string Mobile) : IRequest<IReadOnlyList<ContactDto>>;
}
