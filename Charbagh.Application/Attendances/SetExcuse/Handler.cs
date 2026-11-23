
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Attendances.SetExcuse
{
    public sealed class SetExcuseAttendanceHandler : IRequestHandler<SetExcuseAttendanceCommand, AttendanceDto>
    {
        private readonly IAttendanceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetExcuseAttendanceHandler(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _repository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttendanceDto> Handle(SetExcuseAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (attendance == null)
            {
                throw new AttendanceNotFoundException(request.Id);
            }

            attendance.SetExcuse();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendance.ToDto();
        }
    }

}
