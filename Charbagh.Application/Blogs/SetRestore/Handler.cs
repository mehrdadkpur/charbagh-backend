
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Blogs.SetRestore;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Blogs.SetArchive
{
    public sealed class SetRestoreBlogHandler : IRequestHandler<SetRestoreBlogCommand , BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetRestoreBlogHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <BlogDto> Handle(SetRestoreBlogCommand request , CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

            if (blog == null)
            {
                throw new BlogNotFoundException(request.Id);
            }

            blog.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return blog.ToDto();
        }
    }
}
