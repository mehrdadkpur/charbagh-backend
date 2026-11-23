using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.People;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public sealed class StudentRepository : IStudentRepository
    {
        private readonly CharbaghDbContext _context;

        public StudentRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(Student student, CancellationToken cancellationToken)
        {
            await _context.Students.AddAsync(student, cancellationToken);
        }

        public async Task<bool> ExistsByStudentCodeAsync(string studentCode, CancellationToken cancellationToken)
        {
            return await _context.Students.AnyAsync(x => x.StudentCode == studentCode, cancellationToken);
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Students.AnyAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<bool> ExistsByStudentCodeExceptAsync(string studentCode, Guid? exceptStudentId, CancellationToken cancellationToken)
        {
            return await _context.Students.AnyAsync(x => x.StudentCode == studentCode && (!exceptStudentId.HasValue || x.Id != exceptStudentId.Value), cancellationToken);
        }

        public async Task<bool> ExistsByUserIdExceptAsync(Guid userId, Guid? exceptStudentId, CancellationToken cancellationToken)
        {
            return await _context.Students.AnyAsync(x => x.UserId == userId && (!exceptStudentId.HasValue || x.Id != exceptStudentId.Value), cancellationToken);
        }

        public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(x => x.User)
                .OrderBy(x => x.User.LastName)
                .ToListAsync(cancellationToken);
        }

        public Task<Student?> GetByIdAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return _context.Students
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == studentId, cancellationToken);
        }

        public Task<Student?> GetDetailsByIdAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return _context.Students
                .AsNoTracking()
                .Include(x => x.User)
                    .ThenInclude(x => x.Nationality)
                .Include(x => x.StudentParents)
                    .ThenInclude(x => x.Parent)
                        .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == studentId, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Students.CountAsync(cancellationToken);
        }
    }
}
