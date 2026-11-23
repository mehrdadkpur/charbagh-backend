
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.GetById
{
    public sealed record GetBlogByIdQuery(Guid Id) : IRequest<BlogDto>;
}
