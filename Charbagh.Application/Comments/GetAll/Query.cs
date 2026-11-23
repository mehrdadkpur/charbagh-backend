
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetAll
{
    public sealed record GetAllCommentsQuery() : IRequest<IReadOnlyList< CommentDto>>;
}
