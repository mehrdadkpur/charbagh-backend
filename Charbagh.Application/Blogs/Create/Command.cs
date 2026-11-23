
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.Create
{
    public sealed record CreateBlogCommand(
        Guid UserId,
        Guid CategoryId,
        string Title,
        string Text,
        string? ImageUrl
        ) :IRequest<BlogDto>;
}
