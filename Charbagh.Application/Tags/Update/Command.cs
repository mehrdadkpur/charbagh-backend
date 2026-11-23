using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Update
{
    public sealed record UpdateTagCommand(Guid Id , string Name) : IRequest<TagDto>;
}
