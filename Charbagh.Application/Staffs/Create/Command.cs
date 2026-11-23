
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Create
{
    public sealed record CreateStaffCommand (Guid Id , Guid UserId , DateOnly EmploymentDate, EmploymentType EmploymentType) : IRequest<StaffDto>;
}
