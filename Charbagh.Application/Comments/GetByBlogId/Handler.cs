
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Comments.Common;
using MediatR;

namespace Charbagh.Application.Comments.GetByBlogId
{
    public sealed class GetCommentsByBlogIdHandler : IRequestHandler<GetCommentsByBlogIdQuery, IReadOnlyList<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByBlogIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IReadOnlyList<CommentDto>> Handle(GetCommentsByBlogIdQuery request, CancellationToken cancellationToken)
        {

            var comments = await _commentRepository.GetByBlogIdAsync(request.BlogId,cancellationToken);

            return comments.Select(x => x.ToDto()).ToList();
        }
    }
}
