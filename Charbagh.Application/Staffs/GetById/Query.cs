
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.GetById
{
    public sealed record GetStaffByIdQuery(Guid Id) : IRequest<StaffDto>;
}
