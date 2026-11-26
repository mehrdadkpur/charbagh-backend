using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.GetByLessonSession;

public sealed class GetAttendanceByLessonSessionHandler
    : IRequestHandler<GetAttendanceByLessonSessionQuery, AttendanceDto?>
{
    private readonly IAttendanceRepository _repository;

    public GetAttendanceByLessonSessionHandler(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<AttendanceDto?> Handle(
        GetAttendanceByLessonSessionQuery request,
        CancellationToken cancellationToken)
    {
        var attendance = await _repository.GetByLessonSessionIdAsync(request.LessonSessionId, cancellationToken);
        return attendance?.ToDto();
    }
}
