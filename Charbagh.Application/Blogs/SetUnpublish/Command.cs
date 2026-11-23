
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.SetUnpublish
{
    public sealed record SetUnpublishBlogCommand(Guid Id) : IRequest<BlogDto>;
}
