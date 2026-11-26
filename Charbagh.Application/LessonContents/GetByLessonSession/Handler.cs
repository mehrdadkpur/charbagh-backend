using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetByLessonSession;

public sealed class GetLessonContentsBySessionHandler
    : IRequestHandler<GetLessonContentsBySessionQuery, IReadOnlyList<LessonContentDto>>
{
    private readonly ILessonContentRepository _repository;

    public GetLessonContentsBySessionHandler(ILessonContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LessonContentDto>> Handle(
        GetLessonContentsBySessionQuery request,
        CancellationToken cancellationToken)
    {
        var contents = await _repository.GetByLessonSessionIdAsync(request.LessonSessionId, cancellationToken);
        return contents.Select(item => item.ToDto()).ToList();
    }
}
