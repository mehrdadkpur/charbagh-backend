using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.GetById
{
    public sealed record GetParentByIdQuery(Guid Id) : IRequest<ParentDetailsDto>;
}
