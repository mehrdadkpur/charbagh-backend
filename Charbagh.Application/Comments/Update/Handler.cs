using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Comments.Update
{
    public sealed class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {

            var comment = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            comment.Update(request.Text);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.ToDto();
        }
    }

}
