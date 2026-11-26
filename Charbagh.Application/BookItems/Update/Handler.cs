
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.BookItems.Update
{
    public sealed class UpdateBookItemHandler : IRequestHandler<UpdateBookItemCommand, BookItemDto>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookItemHandler(IBookItemRepository bookItemRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookItemRepository = bookItemRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookItemDto> Handle(UpdateBookItemCommand request, CancellationToken cancellationToken)
        {
            var bookItem = await _bookItemRepository.GetByIdAsync(request.Id, cancellationToken);

            if (bookItem == null)
            {
                throw new BookItemNotFoundException(request.Id);
            }

            var excistBook = await _bookRepository.GetByIdAsync(bookItem.BookId, cancellationToken);
            if (excistBook is null)
            {
                throw new BookNotFoundException(bookItem.BookId);
            }

            bookItem.Update(
                request.Title,
                request.Description,
                request.DisplayOrder,
                request.Number,
                request.Type,
                request.ReferenceType,
                request.RecommendedPracticeDays,
                request.DifficultyLevel,
                request.AudioUrl,
                request.VideoUrl,
                request.PdfUrl
                );
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return bookItem.ToDto();
        }
    }

}
