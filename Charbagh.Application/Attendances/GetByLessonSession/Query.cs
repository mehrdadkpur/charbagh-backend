using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.GetByLessonSession;

public sealed record GetAttendanceByLessonSessionQuery(Guid LessonSessionId)
    : IRequest<AttendanceDto?>;
