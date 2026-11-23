
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Dashboard.GetStats.Common;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Blogs.Create
{
    public sealed class CreateBlogHandler : IRequestHandler<CreateBlogCommand , BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBlogHandler(IBlogRepository blogRepository , IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogDto> Handle(CreateBlogCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId , cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            var blog = Blog.Create(request.UserId, request.Title, request.Text, request.ImageUrl);

            await _blogRepository.AddAsync(blog, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return blog.ToDto();
        }
    }
}
