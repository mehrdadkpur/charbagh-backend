
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.PutOnLeave
{
    public sealed class PutOnLeaveStaffHandler : IRequestHandler<PutOnLeaveStaffCommand, StaffDto>
    {
        private readonly IStaffRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PutOnLeaveStaffHandler(IStaffRepository staffRepository , IUnitOfWork unitOfWork)
        {
            _repository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffDto> Handle (PutOnLeaveStaffCommand request , CancellationToken cancellationToken)
        {
            var staff = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(staff == null)
            {
                throw new StaffNotFoundException();
            }
            staff.PutOnLeave();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return staff.ToDto();
        }
    }
}
