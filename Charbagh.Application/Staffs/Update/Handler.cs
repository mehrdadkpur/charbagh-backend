
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using MediatR;

namespace Charbagh.Application.Staffs.Update
{
    public sealed class UpdateStaffHandler : IRequestHandler<UpdateStaffCommand, StaffDto>
    {
        private readonly IStaffRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStaffHandler(IStaffRepository staffRepository , IUnitOfWork unitOfWork)
        {
            _repository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffDto> Handle (UpdateStaffCommand request , CancellationToken cancellationToken)
        {
            var staff = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(staff == null)
            {
                throw new StaffNotFoundException();
            }

            var existStaff = await _repository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (!existStaff)
            {
                throw new UserAlreadyStaffException(request.UserId);
            }


            staff.Update( request.EmploymentDate , request.EmploymentType);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return staff.ToDto();
        }
    }
}
