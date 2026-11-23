using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Comments.SetReject
{
    public sealed class SetRejectCommentHandler : IRequestHandler<SetRejectCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetRejectCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto> Handle(SetRejectCommentCommand request, CancellationToken cancellationToken)
        {

            var comment = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            comment.Reject();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.ToDto();
        }
    }

}
