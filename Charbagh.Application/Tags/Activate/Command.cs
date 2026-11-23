using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Activate
{
    public sealed record ActivateTagCommand(Guid Id) : IRequest<TagDto>;
}
