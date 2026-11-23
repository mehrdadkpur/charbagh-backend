
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using MediatR;

namespace Charbagh.Application.Blogs.GetByUserId
{
    public sealed class GetBlogsByUserIdHandler : IRequestHandler<GetBlogsByUserIdQuery ,IReadOnlyList<BlogDto>>
    {
        private readonly IBlogRepository _repository;

        public GetBlogsByUserIdHandler(IBlogRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList<BlogDto>> Handle(GetBlogsByUserIdQuery request , CancellationToken cancellationToken)
        {
            var blogs = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);


            return blogs.Select(x => x.ToDto() ).ToList();
        }
    }
}
