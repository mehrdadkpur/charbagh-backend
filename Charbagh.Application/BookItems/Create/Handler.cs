
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using MediatR;

namespace Charbagh.Application.BookItems.Create
{
    public sealed class CreateBookItemHandler : IRequestHandler<CreateBookItemCommand , BookItemDto>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookItemHandler(IBookItemRepository bookItemRepository , IBookRepository bookRepository , IUnitOfWork unitOfWork)
        {
            _bookItemRepository = bookItemRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookItemDto> Handle(CreateBookItemCommand request , CancellationToken cancellationToken)
        {
            var existBook = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

            if (existBook is null)
            {
                throw new BookNotFoundException(request.BookId);
            }

            var exsitsBookItem = await _bookItemRepository.ExistsByBookItemNameAsync(request.Title, cancellationToken);

            if (exsitsBookItem)
            {
               throw new DuplicateBookItemNameException(request.Title);
            }

            var bookItem = BookItem.Create(
                request.BookId,
                request.DisplayOrder,
                request.Number,
                request.Title,
                request.Type,
                request.ReferenceType,
                request.Description,
                request.RecommendedPracticeDays,
                request.DifficultyLevel,
                request.AudioUrl,
                request.VideoUrl,
                request.PdfUrl
                );

            await _bookItemRepository.AddAsync(bookItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return bookItem.ToDto();

        }
    }
}
