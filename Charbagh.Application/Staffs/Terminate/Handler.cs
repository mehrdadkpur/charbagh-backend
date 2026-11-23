
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Terminate
{
    public sealed class TerminateStaffHandler : IRequestHandler<TerminateStaffCommand , StaffDto>
    {
        private readonly IStaffRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TerminateStaffHandler (IStaffRepository staffRepository , IUnitOfWork unitOfWork)
        {
            _repository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffDto> Handle (TerminateStaffCommand request , CancellationToken cancellationToken)
        {
            var staff = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(staff == null)
            {
                throw new StaffNotFoundException();
            }
            staff.Terminate(request.TerminateDate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return staff.ToDto();
        }
    }
}
