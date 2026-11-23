using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetByUserId
{
    public sealed record GetCommentsByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList<CommentDto>>;
}
