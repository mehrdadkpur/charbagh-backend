
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly CharbaghDbContext _context;

        public EvaluationRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }
        public async Task AddAsync(Evaluation evaluation, CancellationToken cancellationToken)
        {
            await _context.Evaluations.AddAsync(evaluation , cancellationToken);
        }

        public async Task<IReadOnlyList<Evaluation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Evaluations.AsNoTracking().OrderBy(x => x.EvaluatedAt).ToListAsync(cancellationToken);
        }

        public async Task<Evaluation?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Evaluations.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<Evaluation?> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken)
        {
            return await _context.Evaluations.FirstOrDefaultAsync(x => x.LessonSessionId == lessonSessionId, cancellationToken);
        }
    }

}
