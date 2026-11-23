using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonContents.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.LessonContents.Create
{
    public sealed class CreateLessonContentHandler : IRequestHandler<CreateLessonContentCommand , LessonContentDto>
    {
        private readonly ILessonContentRepository _lessonContentRepository;
        private readonly ILessonSessionRepository _sessionRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonContentHandler(
            ILessonContentRepository lessonContentRepository , 
            ILessonSessionRepository lessonSessionRepository , 
            IBookItemRepository bookItemRepository , 
            IUnitOfWork unitOfWork
            )
        {
            _lessonContentRepository = lessonContentRepository;
            _sessionRepository = lessonSessionRepository;
            _bookItemRepository = bookItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonContentDto> Handle(CreateLessonContentCommand request , CancellationToken cancellationToken)
        {
            var lessonSession = await _sessionRepository.GetByIdAsync(request.LessonSessionId, cancellationToken);
            var bookItem = await _bookItemRepository.GetByIdAsync(request.BookItemId, cancellationToken);

            if(lessonSession is null)
            {
                throw new LessonSessionNotFoundException(request.LessonSessionId);
            }

            if(bookItem is null)
            {
                throw new BookItemNotFoundException(request.BookItemId);
            }

            var lessonContent = LessonContent.Create(
                request.LessonSessionId,
                request.BookItemId,
                request.FromReference,
                request.ToReference,
                request.TeacherNote
                );

            await _lessonContentRepository.AddAsync(lessonContent, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonContent.ToDto();
        }
    }
}
