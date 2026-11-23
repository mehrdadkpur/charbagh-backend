using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Attendances.SetAbsent
{
    public sealed class SetAbsentAttendanceHandler : IRequestHandler<SetAbsentAttendanceCommand , AttendanceDto>
    {
        private readonly IAttendanceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetAbsentAttendanceHandler(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _repository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttendanceDto> Handle (SetAbsentAttendanceCommand request , CancellationToken cancellationToken)
        {
            var attendance = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if(attendance == null)
            {
                throw new AttendanceNotFoundException(request.Id);
            }

            attendance.SetAbsent();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendance.ToDto();
        }
    }
}
