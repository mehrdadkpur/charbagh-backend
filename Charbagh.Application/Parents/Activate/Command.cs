
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Activate
{
    public sealed record ActivateParentCommand(Guid Id) : IRequest<ParentDto>;
}
