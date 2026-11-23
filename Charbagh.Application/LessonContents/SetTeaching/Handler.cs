
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonContents.Common;
using Charbagh.Application.LessonContents.SetPerformance;
using MediatR;

namespace Charbagh.Application.LessonContents.SetTeaching
{
    public sealed class SetTeachingLessonContentTypeHandler : IRequestHandler<SetTeachingLessonContentTypeCommand, LessonContentDto>
    {
        private readonly ILessonContentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetTeachingLessonContentTypeHandler(ILessonContentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonContentDto> Handle(SetTeachingLessonContentTypeCommand request, CancellationToken cancellationToken)
        {
            var lessonContent = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonContent == null)
            {
                throw new LessonContentNotFoundException(request.Id);
            }

            lessonContent.SetTeaching();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonContent.ToDto();
        }
    }

}
