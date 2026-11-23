
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class LessonContentRepository : ILessonContentRepository
    {
        private readonly CharbaghDbContext _context;

        public LessonContentRepository(CharbaghDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LessonContent lessonContent, CancellationToken cancellationToken)
        {
            await _context.LessonContents.AddAsync(lessonContent, cancellationToken);
        }

        public async Task<IReadOnlyList<LessonContent>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.LessonContents.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<LessonContent?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.LessonContents.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<IReadOnlyList<LessonContent>> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken)
        {
            return await _context.LessonContents.AsNoTracking().Where(x => x.LessonSessionId == lessonSessionId).OrderBy(x => x.Id).ToListAsync(cancellationToken);
        }
    }

}
