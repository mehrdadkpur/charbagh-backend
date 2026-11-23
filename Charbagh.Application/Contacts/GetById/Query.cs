using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetById
{
    public sealed record GetContactByIdQuery (Guid Id) : IRequest<ContactDto>;
}
