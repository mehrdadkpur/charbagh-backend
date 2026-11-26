
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.Update
{
    public sealed class UpdateLessonSessionsHandler: IRequestHandler<UpdateLessonSessionCommand , LessonSessionDto>
    {
        private readonly ILessonSessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonSessionsHandler(ILessonSessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<LessonSessionDto> Handle(UpdateLessonSessionCommand request , CancellationToken cancellationToken)
        {
            var lessonSession = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.Id);
            }

            var sessionExists = await _sessionRepository.ExistsByLessonAssignmentAndDateAsync(\n                request.LessonAssignmentId, request.SessionDate, request.Id, cancellationToken);\n\n            if (sessionExists)\n                throw new DuplicateLessonSessionException(request.LessonAssignmentId, request.SessionDate);\n\n            lessonSession.Update(
                request.SessionDate,
                request.StartTime,
                request.EndTime,
                request.DurationMinutes,
                request.TeacherNote
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonSession.ToDto();
        }
    }
}
