using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonSessions.Common;
using Charbagh.Application.LessonSessions.StudentAbsent;
using MediatR;

namespace Charbagh.Application.LessonSessions.TeacherAbsent
{
    public sealed class TeacherAbsentLessonSessionHandler : IRequestHandler<TeacherAbsentLessonSessionCommand, LessonSessionDto>
    {
        private readonly ILessonSessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherAbsentLessonSessionHandler(ILessonSessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonSessionDto> Handle(TeacherAbsentLessonSessionCommand request, CancellationToken cancellationToken)
        {
            var lessonSession = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.Id);
            }

            lessonSession.TeacherAbsent();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonSession.ToDto();
        }
    }

}
