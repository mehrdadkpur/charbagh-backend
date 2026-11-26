
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Blogs.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Blogs.Create
{
    public sealed class CreateBlogHandler : IRequestHandler<CreateBlogCommand , BlogDto>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public CreateBlogHandler(IBlogRepository blogRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<BlogDto> Handle(CreateBlogCommand request , CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
                throw new UserNotFoundException(userId);

            var blog = Blog.Create(userId, request.CategoryId, request.Title, request.Text, request.ImageUrl);

            await _blogRepository.AddAsync(blog, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BlogDto(
                blog.Id,
                blog.UserId,
                blog.CategoryId,
                user.FirstName,
                user.LastName,
                blog.Title,
                blog.Text,
                blog.ImageUrl,
                blog.Status,
                blog.CreatedAt,
                blog.UpdatedAt);
        }
    }
}
