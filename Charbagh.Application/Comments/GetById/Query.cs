using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetById
{
    public sealed record GetCommentByIdQuery(Guid Id) : IRequest<CommentDto>;
}
