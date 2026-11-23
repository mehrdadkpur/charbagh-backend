
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonContents.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.LessonContents.Update
{
    public sealed class UpdateLessonContentHandler : IRequestHandler<UpdateLessonContentCommand , LessonContentDto>
    {
        private readonly ILessonContentRepository _lessonContentRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonContentHandler(ILessonContentRepository lessonContentRepository , IBookItemRepository bookItemRepository , IUnitOfWork unitOfWork)
        {
            _bookItemRepository = bookItemRepository;
            _unitOfWork = unitOfWork;
            _lessonContentRepository = lessonContentRepository;
        }

        public async Task<LessonContentDto> Handle (UpdateLessonContentCommand request , CancellationToken cancellationToken)
        {
            var lessonContent = await _lessonContentRepository.GetByIdAsync(request.Id , cancellationToken);
            var bookItem = await _bookItemRepository.GetByIdAsync(request.BookItemId, cancellationToken);

            if(lessonContent == null)
            {
                throw new LessonContentNotFoundException(request.Id);
            }

            if(bookItem == null)
            {
                throw new BookItemNotFoundException(request.Id);
            }

            lessonContent.Update(
                request.BookItemId,
                request.FromReference,
                request.ToReference,
                request.TeacherNote
                );

            await _unitOfWork.SaveChangesAsync( cancellationToken );
            return lessonContent.ToDto();
        }
    }
}
