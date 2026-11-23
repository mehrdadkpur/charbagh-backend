
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.SetLate
{
    public sealed record SetLateAttendanceCommand(Guid Id) : IRequest<AttendanceDto>;
}
