
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.PutOnLeave
{
    public sealed record PutOnLeaveStaffCommand(Guid Id) : IRequest<StaffDto>;
}
