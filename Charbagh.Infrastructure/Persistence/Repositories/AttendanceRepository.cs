
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly CharbaghDbContext _context;

        public AttendanceRepository(CharbaghDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Attendance attendance, CancellationToken cancellationToken)
        {
            await _context.Attendances.AddAsync(attendance, cancellationToken);
        }

        public async Task<IReadOnlyList< Attendance>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Attendances.AsNoTracking().OrderBy(x => x.RecordedAt).ToListAsync(cancellationToken);
        }

        public async Task<Attendance?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
           return await _context.Attendances.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<Attendance?> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken) 
        { 
            return await _context.Attendances.FirstOrDefaultAsync(x => x.LessonSessionId == lessonSessionId, cancellationToken); 
        }
    }

}
