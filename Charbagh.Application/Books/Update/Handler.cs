using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Bookitems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Bookitems.Update
{
    public sealed class UpdateBookHandler : IRequestHandler<UpdateBookCommand , BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookHandler(IBookRepository bookRepository,IInstrumentRepository instrumentRepository , IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _instrumentRepository = instrumentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

            if (book is null)
            {
                throw new BookNotFoundException(request.Id);
            }

            var instrument = await _instrumentRepository.GetByIdAsync(request.InstrumentId, cancellationToken);

            if (instrument is null)
            {
                throw new InstrumentNotFoundException(request.InstrumentId);
            }

            var existBookName = await _bookRepository.ExistsByBookNameAsync(request.Title, request.Id , cancellationToken);

            if (existBookName)
            {
                throw new DuplicateBookNameException(request.Title);
            }

            book.Update(
                request.InstrumentId,
                request.Title,
                request.EnglishTitle,
                request.Author,
                request.Publisher,
                request.Volume,
                request.Level,
                request.CoverImageUrl,
                request.DisplayOrder
                );
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return book.ToDto();
        }
    }
}
