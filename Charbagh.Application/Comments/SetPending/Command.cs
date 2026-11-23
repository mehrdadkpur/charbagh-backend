
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.SetPending
{
    public sealed record SetPendingCommentCommand(Guid Id) : IRequest<CommentDto>;
}
