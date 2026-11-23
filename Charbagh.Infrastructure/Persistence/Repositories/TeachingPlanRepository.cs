
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class TeachingPlanRepository : ITeachingPlanRepository
    {
        private readonly CharbaghDbContext _context;

        public TeachingPlanRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }
        public async Task AddAsync(TeachingPlan teachingPlan, CancellationToken cancellationToken)
        {
            await _context.TeachingPlans.AddAsync(teachingPlan, cancellationToken);
        }

        public async Task<bool> ExistsActiveByLessonAssignmentIdAsync(Guid lessonAssignmentId, Guid? excludeTeachingPlanId, CancellationToken cancellationToken)
        {
            return await _context.TeachingPlans.AnyAsync(X => X.LessonAssignmentId == lessonAssignmentId && X.Status == TeachingPlanStatus.Active && (!excludeTeachingPlanId.HasValue || X.Id != excludeTeachingPlanId.Value), cancellationToken);
        }

        public async Task<bool> ExistsByLessonAssignmentAndDisplayOrderAsync(Guid lessonAssignmentId, int displayOrder, Guid? excludeTeachingPlanId, CancellationToken cancellationToken)
        {
            return await _context.TeachingPlans.AnyAsync(X => X.LessonAssignmentId == lessonAssignmentId && X.DisplayOrder == displayOrder && (!excludeTeachingPlanId.HasValue || X.Id != excludeTeachingPlanId.Value) , cancellationToken);
        }

        public async Task<IReadOnlyList<TeachingPlan>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.TeachingPlans.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TeachingPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.TeachingPlans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<TeachingPlan>> GetByLessonAssignmentIdAsync(Guid lessonAssignmentId, CancellationToken cancellationToken)
        {
            return await _context.TeachingPlans.AsNoTracking().Where(x => x.LessonAssignmentId == lessonAssignmentId).OrderBy(x => x.DisplayOrder).ToListAsync(cancellationToken);
        }
    }
}
