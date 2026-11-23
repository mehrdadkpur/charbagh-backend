using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.SetReject
{
    public sealed record SetRejectCommentCommand(Guid Id) : IRequest<CommentDto>;
}
