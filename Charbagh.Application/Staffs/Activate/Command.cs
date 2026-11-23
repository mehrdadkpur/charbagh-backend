
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Activate
{
    public sealed record ActivateStaffCommand(Guid Id) : IRequest<StaffDto>
    {
    }
}
