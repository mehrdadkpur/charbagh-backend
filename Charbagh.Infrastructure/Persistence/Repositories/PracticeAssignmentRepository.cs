
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class PracticeAssignmentRepository : IPracticeAssignmentRepository
    {
        private readonly CharbaghDbContext _context;

        public PracticeAssignmentRepository(CharbaghDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PracticeAssignment practiceAssignment, CancellationToken cancellationToken)
        {
            await _context.PracticeAssignments.AddAsync(practiceAssignment, cancellationToken);
        }

        public async Task<IReadOnlyList< PracticeAssignment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.PracticeAssignments.AsNoTracking().OrderBy(x => x.DueDate).ToListAsync(cancellationToken);
        }

        public async Task<PracticeAssignment?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
           return await _context.PracticeAssignments.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<PracticeAssignment?> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken) 
        { 
            return await _context.PracticeAssignments.FirstOrDefaultAsync(x => x.LessonSessionId == lessonSessionId, cancellationToken); 
        }
    }

}
