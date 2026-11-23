
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetById
{
    public sealed class GetLessonSessionByIdHandler : IRequestHandler<GetLessonSessionByIdQuery , LessonSessionDto>
    {
        private readonly ILessonSessionRepository _sessionRepository;

        public GetLessonSessionByIdHandler(ILessonSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<LessonSessionDto> Handle (GetLessonSessionByIdQuery request , CancellationToken cancellationToken)
        {
            var lessonSession = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.Id);
            }

            return lessonSession.ToDto();
        }
    }
}
