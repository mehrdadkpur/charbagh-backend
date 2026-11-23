using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.Update
{
    public sealed record UpdateBlogCommand(
           Guid Id,
           string Title,
           string Text,
           string? ImageUrl
           ) : IRequest<BlogDto>;
}
