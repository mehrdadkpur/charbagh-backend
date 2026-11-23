
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Blogs.GetById
{
    public sealed class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery , BlogDto>
    {
        private readonly IBlogRepository _repository;

        public GetBlogByIdHandler(IBlogRepository repository)
        {
            _repository = repository; 
        }

        public async Task <BlogDto> Handle(GetBlogByIdQuery request , CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(blog == null)
            {
                throw new BlogNotFoundException(request.Id);
            }

            return blog.ToDto();
        }
    }
}
