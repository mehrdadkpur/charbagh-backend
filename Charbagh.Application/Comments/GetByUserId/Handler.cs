
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetByUserId
{
    public sealed class GetCommentsByUserIdHandler : IRequestHandler<GetCommentsByUserIdQuery, IReadOnlyList<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByUserIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IReadOnlyList<CommentDto>> Handle(GetCommentsByUserIdQuery request, CancellationToken cancellationToken)
        {

            var comments = await _commentRepository.GetByBlogIdAsync(request.UserId, cancellationToken);

            return comments.Select(x => x.ToDto()).ToList();
        }
    }
}
