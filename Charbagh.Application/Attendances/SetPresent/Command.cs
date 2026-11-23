
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.SetPresent
{
    public sealed record SetPresentAttendanceCommand(Guid Id) : IRequest<AttendanceDto>;
}
