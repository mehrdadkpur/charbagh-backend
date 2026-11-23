
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.SetRestore
{
    public sealed record SetRestoreBlogCommand(Guid Id) : IRequest<BlogDto>;
}
