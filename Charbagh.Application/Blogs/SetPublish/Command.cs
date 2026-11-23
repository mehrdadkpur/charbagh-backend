
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.SetPublish
{
    public sealed record SetPublishBlogCommand(Guid Id) : IRequest<BlogDto>;
}
