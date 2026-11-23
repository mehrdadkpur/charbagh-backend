using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Deactivate
{
    public sealed record DeactivateTagCommand(Guid Id) : IRequest<TagDto>;
}
