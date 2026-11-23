using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Comments.Create
{
    public sealed class CreateCommentHandler: IRequestHandler<CreateCommentCommand,CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentHandler(ICommentRepository commentRepository , IUserRepository userRepository , IBlogRepository blogRepository , IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <CommentDto> Handle(CreateCommentCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            var blog = await _blogRepository.GetByIdAsync (request.BlogId, cancellationToken);

            if (blog == null)
            {
                throw new BlogNotFoundException(request.BlogId);
            }

            var comment = Comment.Create(request.UserId , request.BlogId , request.Text);

            await _commentRepository.AddAsync(comment , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.ToDto();
        }
    }
}
