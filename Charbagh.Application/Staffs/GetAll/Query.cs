
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.GetAll
{
    public sealed record GetAllStaffsQuery : IRequest<IReadOnlyList<StaffDto>>;
}
