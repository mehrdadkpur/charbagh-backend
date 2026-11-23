using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Branches;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class ClassroomRepository : IClassroomRepository
{
    private readonly CharbaghDbContext _context;

    public ClassroomRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Classroom classroom,
        CancellationToken cancellationToken)
    {
        await _context.Classrooms.AddAsync(
            classroom,
            cancellationToken);
    }

    public async Task<Classroom?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Classrooms
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Classroom?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Classrooms
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Classroom>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Classrooms
            .AsNoTracking()
            .OrderBy(x => x.BranchId)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Classroom>> GetByBranchIdAsync(
        Guid branchId,
        CancellationToken cancellationToken)
    {
        return await _context.Classrooms
            .AsNoTracking()
            .Where(x => x.BranchId == branchId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(
        Guid branchId,
        string code,
        CancellationToken cancellationToken)
    {
        return await _context.Classrooms
            .AnyAsync(
                x => x.BranchId == branchId &&
                     x.Code == code,
                cancellationToken);
    }
}