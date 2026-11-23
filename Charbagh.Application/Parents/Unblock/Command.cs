using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Unblock
{
    public sealed record UnblockParentCommand(Guid Id): IRequest<ParentDto>;
}
