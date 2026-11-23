
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.SetPerformance
{
    public sealed class SetPerformanceLessonContentTypeHandler : IRequestHandler<SetPerformanceLessonContentTypeCommand , LessonContentDto>
    {
        private readonly ILessonContentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPerformanceLessonContentTypeHandler(ILessonContentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonContentDto> Handle (SetPerformanceLessonContentTypeCommand request , CancellationToken cancellationToken)
        {
            var lessonContent = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonContent == null)
            {
                throw new LessonContentNotFoundException(request.Id);
            }

            lessonContent.SetPerformance();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonContent.ToDto();
        }
    }
}
