
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.GetById
{
    public sealed record GetAttendanceByIdQuery(Guid Id) : IRequest<AttendanceDto>;
}
