
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetById
{
    public sealed class GetLessonContentByIdHandler : IRequestHandler<GetLessonContentByIdQuery , LessonContentDto>
    {
        private readonly ILessonContentRepository _repository;

        public GetLessonContentByIdHandler (ILessonContentRepository repository)
        {
            _repository = repository;
        }

        public async Task<LessonContentDto> Handle (GetLessonContentByIdQuery request , CancellationToken cancellationToken)
        {
            var lessonContent = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(lessonContent == null)
            {
                throw new LessonContentNotFoundException(request.Id);
            }

            return lessonContent.ToDto();
        }
    }
}
