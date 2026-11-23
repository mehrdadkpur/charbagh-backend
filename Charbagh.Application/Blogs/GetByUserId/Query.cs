
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.GetByUserId
{
    public sealed record GetBlogsByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList<BlogDto>>;
}
