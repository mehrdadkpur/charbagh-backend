
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Update
{
    public sealed record UpdateStaffCommand(Guid Id , Guid UserId, DateOnly EmploymentDate, EmploymentType EmploymentType) : IRequest<StaffDto>;
}
