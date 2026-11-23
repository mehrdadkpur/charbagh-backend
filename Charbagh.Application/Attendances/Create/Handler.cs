
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.Attendances.Create
{
    public sealed class CreateAttendanceHandler : IRequestHandler<CreateAttendanceCommand , AttendanceDto>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILessonSessionRepository _lessonSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttendanceHandler(IAttendanceRepository attendanceRepository, ILessonSessionRepository lessonSessionRepository, IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _lessonSessionRepository = lessonSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AttendanceDto> Handle(CreateAttendanceCommand request , CancellationToken cancellationToken)
        {
            var lessonSession = await _lessonSessionRepository.GetByIdAsync(request.LessonSessionId, cancellationToken);

            if( lessonSession == null )
            {
                throw new LessonSessionNotFoundException(request.LessonSessionId);
            }

            var attendance = Attendance.Create(
                request.LessonSessionId,
                request.Status,
                request.CheckInAt,
                request.CheckOutAt,
                request.Reason,
                request.Note,
                request.RecordedByUserId
                );

            await _attendanceRepository.AddAsync(attendance, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return attendance.ToDto();
        }
    }
}
