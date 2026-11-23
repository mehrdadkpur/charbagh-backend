
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.GetAll
{
    public sealed record GetAllParentsQuery : IRequest<IReadOnlyList<ParentDto>>;
}
