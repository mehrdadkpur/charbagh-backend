
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Attendances.SetAbsent;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Attendances.SetPresent
{
    public sealed class SetPresentAttendanceHandler : IRequestHandler<SetPresentAttendanceCommand, AttendanceDto>
    {
        private readonly IAttendanceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPresentAttendanceHandler(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _repository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttendanceDto> Handle(SetPresentAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (attendance == null)
            {
                throw new AttendanceNotFoundException(request.Id);
            }

            attendance.SetPresent();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendance.ToDto();
        }
    }

}
