
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetAll
{
    public sealed class GetAllLessonSessionsHandler : IRequestHandler<GetAllLessonSessionsQuery ,IReadOnlyList< LessonSessionDto>>
    {
        private readonly ILessonSessionRepository _sessionRepository;


        public GetAllLessonSessionsHandler(ILessonSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task <IReadOnlyList<LessonSessionDto>> Handle(GetAllLessonSessionsQuery request , CancellationToken cancellationToken)
        {
            var lessonSessions = await _sessionRepository.GetAllAsync(cancellationToken);

            return lessonSessions.Select(x => x.ToDto()).ToList();
        }
    }
}
