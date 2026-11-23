using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Domain.Entities.People;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class TeacherInstrumentRepository: ITeacherInstrumentRepository
{
    private readonly CharbaghDbContext _context;

    public TeacherInstrumentRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        TeacherInstrument teacherInstrument,
        CancellationToken cancellationToken)
    {
        await _context.TeacherInstruments.AddAsync(
            teacherInstrument,
            cancellationToken);
    }

    public async Task<TeacherInstrument?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<TeacherInstrument?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<TeacherInstrument>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .AsNoTracking()
            .OrderBy(x => x.TeacherId)
            .ThenBy(x => x.InstrumentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TeacherInstrument>> GetByTeacherIdAsync(
        Guid teacherId,
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .AsNoTracking()
            .Where(x => x.TeacherId == teacherId)
            .OrderBy(x => x.InstrumentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TeacherInstrument>> GetByInstrumentIdAsync(
        Guid instrumentId,
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .AsNoTracking()
            .Where(x => x.InstrumentId == instrumentId)
            .OrderBy(x => x.TeacherId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid teacherId,
        Guid instrumentId,
        CancellationToken cancellationToken)
    {
        return await _context.TeacherInstruments
            .AnyAsync(
                x => x.TeacherId == teacherId &&
                     x.InstrumentId == instrumentId,
                cancellationToken);
    }
}