
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.Update
{
    public sealed record UpdateCommentCommand(Guid Id , string Text) : IRequest<CommentDto>;
}
