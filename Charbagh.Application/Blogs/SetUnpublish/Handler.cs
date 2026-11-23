
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Blogs.SetUnpublish
{
    public sealed class SetUnpublishBlogHandler : IRequestHandler<SetUnpublishBlogCommand, BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetUnpublishBlogHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <BlogDto> Handle(SetUnpublishBlogCommand request , CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

            if (blog == null)
            {
                throw new BlogNotFoundException(request.Id);
            }

            blog.Unpublish();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return blog.ToDto();
        }
    }
}
