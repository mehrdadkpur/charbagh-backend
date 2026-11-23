
namespace Charbagh.Application.Comments.Common
{
    public sealed record CommentDto(
        Guid Id ,
        Guid UserId,
        Guid BlogId,
        string Text,
        CommentStatus Status
        );
}
