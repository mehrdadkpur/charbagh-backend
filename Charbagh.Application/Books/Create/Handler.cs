
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Bookitems.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using MediatR;

namespace Charbagh.Application.Bookitems.Create
{
    public sealed class CreateBookHandler: IRequestHandler<CreateBookCommand , BookDto>
    {
        IBookRepository _bookRepository;
        IInstrumentRepository _instrumentRepository;
        IUnitOfWork _unitOfWork;

        public CreateBookHandler(IBookRepository bookRepository, IInstrumentRepository instrumentRepository , IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _instrumentRepository = instrumentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(CreateBookCommand request , CancellationToken cancellationToken)
        {
            var instrument = await _instrumentRepository.GetByIdAsync(request.InstrumentId, cancellationToken);

            if (instrument is null)
            {
                throw new InstrumentNotFoundException(request.InstrumentId);
            }

            var existsBook = await _bookRepository.ExistsByBookNameAsync(request.Title, null, cancellationToken);

            if (existsBook)
            {
                throw new DuplicateBookNameException(request.Title);
            }

            var book = Book.Create(request.InstrumentId , request.Title , request.EnglishTitle , request.Author , request.Publisher , request.Volume , request.Level ,request.CoverImageUrl ,request.DisplayOrder);

            await _bookRepository.AddAsync(book , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return book.ToDto();
        }
    }
}
