using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

public sealed class UserRepository : IUserRepository
{
    private readonly CharbaghDbContext _context;

    public UserRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Users
            .Include(user => user.Nationality)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public Task<User?> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Users
            .AsNoTracking()
            .AsSplitQuery()
            .Include(user => user.Nationality)
            .Include(user => user.Student)
            .Include(user => user.Teacher)
            .Include(user => user.Parent)
            .Include(user => user.Staff)
            .Include(user => user.UserBranches)
                .ThenInclude(userBranch => userBranch.Branch)
            .Include(user => user.UserBranches)
                .ThenInclude(userBranch => userBranch.UserBranchRoles)
                    .ThenInclude(userBranchRole => userBranchRole.Role)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Nationality)
            .OrderBy(user => user.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByNationalIdentificationNumberAsync(string nationalIdentificationNumber, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(
            user => user.NationalIdentificationNumber == nationalIdentificationNumber,
            cancellationToken);
    }

    public async Task<User?> GetByMobileAsync(string mobile, CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Mobile == mobile, cancellationToken);
    }

    public Task<string?> GetSecurityStampByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return _context.Users
            .AsNoTracking()
            .Where(user => user.Id == id)
            .Select(user => user.SecurityStamp)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsNationalIdentificationNumberAsync(string nationalIdentificationNumber, Guid? excludeUserId, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(
            user => user.NationalIdentificationNumber == nationalIdentificationNumber && user.Id != excludeUserId,
            cancellationToken);
    }

    public async Task<bool> ExistsByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsNationalityIdAsync(Guid nationalityId, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(user => user.NationalityId == nationalityId, cancellationToken);
    }

    public Task<bool> ExistsByMobileAsync(string mobile, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(user => user.Mobile == mobile, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.CountAsync(cancellationToken);
    }
}
