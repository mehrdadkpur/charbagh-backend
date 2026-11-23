using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Block
{
    public sealed record BlockParentCommand(Guid Id): IRequest<ParentDto>;
}
