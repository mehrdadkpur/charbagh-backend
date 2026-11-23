
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.GetAll
{
    public sealed record GetAllBlogsQuery : IRequest<IReadOnlyList<BlogDto>>;
}
