using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetAll
{
    public sealed class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery ,IReadOnlyList< CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllCommentsHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IReadOnlyList< CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
      
            var comments =await _commentRepository.GetAllAsync(cancellationToken);

            return comments.Select(x => x.ToDto()).ToList();
        }
    }
}
