using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Suspend
{
    public sealed record SuspendParentCommand(Guid Id): IRequest<ParentDto>;
}
