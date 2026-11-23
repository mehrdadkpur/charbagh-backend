using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.People;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public sealed class TeacherRepository : ITeacherRepository
    {

        private readonly CharbaghDbContext _context;

        public TeacherRepository (CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }
        public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            await _context.Teachers.AddAsync(teacher, cancellationToken);
        }

        public async Task<bool> ExistsByTeacherCodeAsync(string teacherCode, CancellationToken cancellationToken)
        {
            return await _context.Teachers.AnyAsync(x => x.PersonnelCode == teacherCode , cancellationToken);
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Teachers.AnyAsync(x => x.UserId == userId , cancellationToken);
        }

        public async Task<bool> ExistsByTeacherCodeExceptAsync(string teacherCode, Guid? exceptTeacherId, CancellationToken cancellationToken)
        {
            return await _context.Teachers.AnyAsync(x => x.PersonnelCode == teacherCode && (!exceptTeacherId.HasValue || x.Id != exceptTeacherId.Value), cancellationToken);
        }

        public async Task<bool> ExistsByUserIdExceptAsync(Guid userId, Guid? exceptTeacherId, CancellationToken cancellationToken)
        {
            return await _context.Teachers.AnyAsync(x => x.UserId == userId && (!exceptTeacherId.HasValue || x.Id != exceptTeacherId.Value), cancellationToken);
        }

        public async Task<IReadOnlyList<Teacher>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Include(x=>x.User)
                .OrderBy(x => x.User.LastName)
                .ToListAsync(cancellationToken);
        }

        public async Task<Teacher?> GetByIdAsync( Guid id, CancellationToken cancellationToken)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .FirstOrDefaultAsync(
                    t => t.Id == id,
                    cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
           return await _context.Teachers.CountAsync(cancellationToken) ;
        }
    }
}
