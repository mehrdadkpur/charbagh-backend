using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.GetAll
{
    public sealed class GetAllBlogsHandler : IRequestHandler<GetAllBlogsQuery , IReadOnlyList<BlogDto>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetAllBlogsHandler (IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IReadOnlyList<BlogDto>> Handle (GetAllBlogsQuery request , CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetAllAsync (cancellationToken);

            return blogs.Select(x => x.ToDto()).ToList();
        }
    }
}
