
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonSessions.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.LessonSessions.Create
{
    public sealed class CreateLessonSessionHandler : IRequestHandler<CreateLessonSessionCommand , LessonSessionDto>
    {
        private readonly ILessonSessionRepository _sessionRepository;
        private readonly ILessonAssignmentRepository _assignmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonSessionHandler(ILessonSessionRepository sessionRepository, ILessonAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _assignmentRepository = assignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonSessionDto> Handle(CreateLessonSessionCommand request , CancellationToken cancellationToken)
        {
            var lessonAssignment = await _assignmentRepository.GetByIdAsync(request.LessonAssignmentId, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.LessonAssignmentId);
            }

            var lessonSession = LessonSession.Create(
                request.LessonAssignmentId,
                request.SessionDate,
                request.StartTime,
                request.EndTime,
                request.DurationMinutes,
                request.TeacherNote
                );

            await _sessionRepository.AddAsync(lessonSession, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return lessonSession.ToDto();
        }
    }
}
