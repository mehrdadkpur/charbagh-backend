
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Activate
{
    public sealed class ActivateStaffHandler : IRequestHandler<ActivateStaffCommand , StaffDto>
    {
        private readonly IStaffRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateStaffHandler (IStaffRepository staffRepository , IUnitOfWork unitOfWork)
        {
            _repository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffDto> Handle (ActivateStaffCommand request , CancellationToken cancellationToken)
        {
            var staff = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(staff == null)
            {
                throw new StaffNotFoundException();
            }
            staff.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return staff.ToDto();
        }
    }
}
