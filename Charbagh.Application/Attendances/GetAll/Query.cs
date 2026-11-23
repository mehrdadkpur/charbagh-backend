
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.GetAll
{
    public sealed record GetAllAttendancesQuery(): IRequest<IReadOnlyList<AttendanceDto>>;
}
