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

    public async Task AddAsync( User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync( user, cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return _context.Users
            .Include(user => user.Nationality)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<User>> GetAllAsync( CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Nationality)
            .OrderBy(user => user.LastName)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByNationalIdentificationNumberAsync( string nationalIdentificationNumber, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.NationalIdentificationNumber == nationalIdentificationNumber , cancellationToken);
    }

    public async Task<User?> GetByMobileAsync( string mobile,CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync( x => x.Mobile == mobile, cancellationToken);
    }

    public async Task<bool> ExistsNationalIdentificationNumberAsync(string nationalIdentificationNumber, Guid? excludeUserId, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(x => x.NationalIdentificationNumber == nationalIdentificationNumber && x.Id != excludeUserId , cancellationToken);
    }

    public async Task<bool> ExistsByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsNationalityIdAsync(Guid nationalityId, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(x => x.NationalityId == nationalityId, cancellationToken);
    }

    public Task<bool> ExistsByMobileAsync( string mobile, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync( x => x.Mobile == mobile, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.CountAsync(cancellationToken);
    }
}