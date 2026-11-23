
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Blogs.Update
{
    public sealed class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
         private readonly IUnitOfWork _unitOfWork;

        public UpdateBlogHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogDto> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

            if (blog == null)
            {
                throw new BlogNotFoundException(request.Id);
            }

            blog.Update( request.Title, request.Text, request.ImageUrl);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return blog.ToDto();
        }
    }

}
