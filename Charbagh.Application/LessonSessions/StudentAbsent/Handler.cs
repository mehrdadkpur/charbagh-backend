
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.StudentAbsent
{
    public sealed class StudentAbsentLessonSessionHandler : IRequestHandler<StudentAbsentLessonSessionCommand, LessonSessionDto>
    {
        private readonly ILessonSessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentAbsentLessonSessionHandler(ILessonSessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonSessionDto> Handle(StudentAbsentLessonSessionCommand request, CancellationToken cancellationToken)
        {
            var lessonSession = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.Id);
            }

            lessonSession.StudentAbsent();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonSession.ToDto();
        }
    }

}
