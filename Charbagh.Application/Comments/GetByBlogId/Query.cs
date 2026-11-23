
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetByBlogId
{
    public sealed record GetCommentsByBlogIdQuery(Guid BlogId) : IRequest<IReadOnlyList<CommentDto>>;
}
