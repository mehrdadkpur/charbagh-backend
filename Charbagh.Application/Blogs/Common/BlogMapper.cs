
using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Blogs.Common
{
    public static class BlogMapper
    {
        public static BlogDto ToDto (this Blog blog)
        {
            return new BlogDto(
                blog.Id,
                blog.UserId,
                blog.CategoryId,
                blog.Author.FirstName,
                blog.Author.LastName,
                blog.Title,
                blog.Text,
                blog.ImageUrl,
                blog.Status,
                blog.CreatedAt,
                blog.UpdatedAt
                );
        }
    }
}
