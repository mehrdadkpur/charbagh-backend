using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.Create
{
    public sealed record CreateContactCommand(
        string FullName,
        string Mobile
        ) :IRequest<ContactDto>;
}
