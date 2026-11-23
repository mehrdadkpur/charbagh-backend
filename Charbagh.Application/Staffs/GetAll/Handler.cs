
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.GetAll
{
    public sealed class GetAllStaffsHandler : IRequestHandler<GetAllStaffsQuery , IReadOnlyList<StaffDto>>
    {
        private readonly IStaffRepository _repository;

        public GetAllStaffsHandler(IStaffRepository repository)
        {
            _repository = repository; 
        }

        public async Task<IReadOnlyList<StaffDto>> Handle(GetAllStaffsQuery query , CancellationToken cancellationToken)
        {
            var staffs = await _repository.GetAllAsync(cancellationToken);

            return staffs.Select(x => x.ToDto()).ToList();
        }
    }
}
