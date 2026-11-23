using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class LessonAssignmentRepository : ILessonAssignmentRepository
    {
        private readonly CharbaghDbContext _context;

        public LessonAssignmentRepository(CharbaghDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LessonAssignment lessonAssignment, CancellationToken cancellationToken)
        {
            await _context.LessonAssignments.AddAsync(lessonAssignment, cancellationToken);
        }

        public async Task<bool> ExistsActiveAssignmentAsync(Guid studentId, Guid instrumentId, Guid? excludeLessonAssignmentId, CancellationToken cancellationToken)
        {
            return await _context.LessonAssignments.AnyAsync(x => x.StudentId == studentId && x.InstrumentId == instrumentId && x.Status == LessonAssignmentStatus.Active &&  (!excludeLessonAssignmentId.HasValue || x.Id == excludeLessonAssignmentId.Value ) , cancellationToken);
        }

        public async Task<IReadOnlyList<LessonAssignment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.LessonAssignments.AsNoTracking().ToListAsync();
        }

        public async Task<LessonAssignment?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.LessonAssignments.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<LessonAssignment?> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken)
        {
            return await _context.LessonAssignments.FirstOrDefaultAsync(x => x.StudentId == StudentId, cancellationToken);
        }
    }
}
