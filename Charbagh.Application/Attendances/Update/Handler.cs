
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Attendances.Update
{
    public sealed class UpdateAttendanceHandler : IRequestHandler<UpdateAttendanceCommand, AttendanceDto>
    {
        private readonly IAttendanceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAttendanceHandler(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _repository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttendanceDto> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (attendance == null)
            {
                throw new AttendanceNotFoundException(request.Id);
            }

            attendance.Update(
                request.CheckInAt,
                request.CheckOutAt,
                request.Reason,
                request.Note
                );
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendance.ToDto();
        }
    }

}
