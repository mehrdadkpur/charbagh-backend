
using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Comments.Common
{
    public static class CommentMapper
    {
        public static CommentDto ToDto (this Comment comment)
        {
            return new CommentDto(
                comment.Id, 
                comment.UserId, 
                comment.BlogId, 
                comment.Text, 
                comment.Status
                );
        }
    }
}
