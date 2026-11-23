using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.SetAbsent
{
    public sealed record SetAbsentAttendanceCommand(Guid Id) : IRequest<AttendanceDto>;
}
