
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Bookitems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Bookitems.Deactivate
{
    public sealed class DeactivateBookHandler : IRequestHandler<DeactivateBookCommand , BookDto>
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateBookHandler(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(DeactivateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
            {
                throw new BookNotFoundException(request.Id);
            }

            book.Deactivate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return book.ToDto();
        }
    }
}
