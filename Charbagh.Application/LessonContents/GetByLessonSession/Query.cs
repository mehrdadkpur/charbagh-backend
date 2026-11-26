using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetByLessonSession;

public sealed record GetLessonContentsBySessionQuery(Guid LessonSessionId)
    : IRequest<IReadOnlyList<LessonContentDto>>;
