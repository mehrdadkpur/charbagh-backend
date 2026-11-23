
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Create
{
    public sealed record CreateParentCommand(Guid UserId , ParentStatus Status) : IRequest<ParentDto>;
}
