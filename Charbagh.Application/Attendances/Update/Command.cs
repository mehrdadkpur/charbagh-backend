
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.Update
{
    public sealed record UpdateAttendanceCommand(
        Guid Id,
        DateTime? CheckInAt,
        DateTime? CheckOutAt,
        string? Reason,
        string? Note
        ): IRequest<AttendanceDto>;
}
