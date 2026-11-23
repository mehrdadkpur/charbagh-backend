
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.SetApprove
{
    public sealed record SetApproveCommentCommand(Guid Id) : IRequest<CommentDto>;
}
