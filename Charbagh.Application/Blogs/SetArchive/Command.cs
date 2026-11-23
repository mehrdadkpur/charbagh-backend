
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.SetArchive
{
    public sealed record SetArchiveBlogCommand(Guid Id) : IRequest<BlogDto>;
}
