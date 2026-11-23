using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class StudentParentRepository : IStudentParentRepository
    {
        private readonly CharbaghDbContext _charbaghDbContext;

        public StudentParentRepository(CharbaghDbContext context)
        {
            _charbaghDbContext = context;
        }
        public async Task AddAsync(StudentParent studentParent, CancellationToken cancellationToken)
        {
            await _charbaghDbContext.StudentParents.AddAsync(studentParent , cancellationToken);
        }
        public async Task<bool> ExistsByStudentAndParentAsync(Guid studentId , Guid ParentId , CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.StudentParents.AnyAsync(x => x.StudentId == studentId &&  x.ParentId == ParentId , cancellationToken);
        }

        public Task<bool> ExistsByParentId(Guid ParentId, CancellationToken cancellationToken)
        {
            return _charbaghDbContext.StudentParents.AnyAsync(x => x.ParentId == ParentId , cancellationToken);
        }

        public Task<bool> ExistsByStudentId(Guid StudentId, CancellationToken cancellationToken)
        {
           return _charbaghDbContext.StudentParents.AnyAsync(x => x.StudentId == StudentId , cancellationToken);
        }

        public async Task<IReadOnlyList<StudentParent>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.StudentParents.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<StudentParent?> GetByIdAsync(Guid StudentParentId, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.StudentParents.FirstOrDefaultAsync(x => x.Id == StudentParentId, cancellationToken);
        }

        public async Task<StudentParent?> GetByParentIdAsync(Guid ParentId, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.StudentParents.FirstOrDefaultAsync(x => x.ParentId == ParentId, cancellationToken);
        }

        public async Task<StudentParent?> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.StudentParents.FirstOrDefaultAsync(x => x.StudentId == StudentId, cancellationToken);
        }

        public async Task<StudentParent?> RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var studentParent =await _charbaghDbContext.StudentParents.FirstOrDefaultAsync(x =>x.Id == Id, cancellationToken);
            if (studentParent is null)
            {
                return null;
            }
            _charbaghDbContext.StudentParents.Remove(studentParent);
            return studentParent;
        }
    }
}
