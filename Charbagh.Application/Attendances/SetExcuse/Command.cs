using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.SetExcuse
{
    public sealed record SetExcuseAttendanceCommand(Guid Id) : IRequest<AttendanceDto>;
}
