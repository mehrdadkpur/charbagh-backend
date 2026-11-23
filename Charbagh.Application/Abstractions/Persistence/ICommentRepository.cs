using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment, CancellationToken cancellationToken);

        Task<Comment?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Comment>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList< Comment>> GetByUserIdAsync(Guid UserId, CancellationToken cancellationToken);

        Task<IReadOnlyList<Comment>> GetByBlogIdAsync(Guid BlogId, CancellationToken cancellationToken);

    }
}
