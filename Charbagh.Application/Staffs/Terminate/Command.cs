
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Terminate
{
    public sealed record TerminateStaffCommand(Guid Id , DateOnly TerminateDate) : IRequest<StaffDto>;
}
