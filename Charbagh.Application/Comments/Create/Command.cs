
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.Create
{
    public sealed record CreateCommentCommand(
        Guid UserId,
        Guid BlogId,
        string Text
        ): IRequest<CommentDto>;
}
