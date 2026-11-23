
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.GetById
{
    public sealed class GetStaffByIdHandler: IRequestHandler<GetStaffByIdQuery , StaffDto>
    {
        private readonly IStaffRepository _repository;

        public GetStaffByIdHandler(IStaffRepository repository)
        {
            _repository = repository;
        }

        public async Task<StaffDto> Handle (GetStaffByIdQuery request , CancellationToken cancellationToken)
        {
            var staff = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (staff == null)
            {
                throw new StaffNotFoundException();
            }

            return staff.ToDto();
        }
    }
}
