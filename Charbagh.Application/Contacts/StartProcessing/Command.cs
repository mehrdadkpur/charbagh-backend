using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.StartProcessing
{
    public sealed record StartProcessingContactCommand(Guid Id) : IRequest<ContactDto>;
}
