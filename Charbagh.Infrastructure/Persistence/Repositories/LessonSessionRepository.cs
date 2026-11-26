
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class LessonSessionRepository : ILessonSessionRepository
    {
        private readonly CharbaghDbContext _context;

        public LessonSessionRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(LessonSession lessonSession, CancellationToken cancellationToken)
        {
           await _context.LessonSessions.AddAsync(lessonSession,cancellationToken);
        }

        public async Task<bool> ExistsByLessonAssignmentAndDateAsync(Guid lessonAssignmentId, DateTime sessionDate, Guid? excludeLessonSessionId, CancellationToken cancellationToken)
        {
            return await _context.LessonSessions.AnyAsync(
                x => x.LessonAssignmentId == lessonAssignmentId
                     && x.SessionDate == sessionDate
                     && (!excludeLessonSessionId.HasValue || x.Id != excludeLessonSessionId.Value),
                cancellationToken);
        }

        public async Task<IReadOnlyList<LessonSession>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.LessonSessions.AsNoTracking().OrderBy(x => x.SessionDate).ToListAsync(cancellationToken);
        }

        public async Task<LessonSession?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.LessonSessions.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<IReadOnlyList<LessonSession>> GetByLessonAssignmentIdAsync(Guid lessonAssignmentId, CancellationToken cancellationToken) 
        { 
            return await _context.LessonSessions.AsNoTracking().Where(x => x.LessonAssignmentId == lessonAssignmentId).OrderBy(x => x.SessionDate).ThenBy(x => x.StartTime).ToListAsync(cancellationToken); 
        }
    }
}
