
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetAll
{
    public sealed class GetAllLessonContentsHandler : IRequestHandler<GetAllLessonContentsQuery ,IReadOnlyList< LessonContentDto>>
    {
        private readonly ILessonContentRepository _repository;

        public GetAllLessonContentsHandler (ILessonContentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<LessonContentDto>> Handle (GetAllLessonContentsQuery request , CancellationToken cancellationToken)
        {
            var lessonContents = await _repository.GetAllAsync (cancellationToken);

            return lessonContents.Select(x => x.ToDto()).ToList();
        }
    }
}
