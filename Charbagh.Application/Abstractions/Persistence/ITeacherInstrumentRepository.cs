using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Abstractions.Persistence.Repositories.People;

public interface ITeacherInstrumentRepository
{
    Task AddAsync(
        TeacherInstrument teacherInstrument,
        CancellationToken cancellationToken);

    Task<TeacherInstrument?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<TeacherInstrument?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TeacherInstrument>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TeacherInstrument>> GetByTeacherIdAsync(
        Guid teacherId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TeacherInstrument>> GetByInstrumentIdAsync(
        Guid instrumentId,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        Guid teacherId,
        Guid instrumentId,
        CancellationToken cancellationToken);
}