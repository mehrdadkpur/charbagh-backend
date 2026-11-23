using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Create
{
    public sealed record CreateTagCommand(string Name) : IRequest<TagDto>;
}
