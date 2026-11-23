using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Comments.SetPending
{
    public sealed class SetPendingCommentHandler : IRequestHandler<SetPendingCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPendingCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto> Handle(SetPendingCommentCommand request, CancellationToken cancellationToken)
        {

            var comment = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            comment.SetPending();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.ToDto();
        }
    }

}
