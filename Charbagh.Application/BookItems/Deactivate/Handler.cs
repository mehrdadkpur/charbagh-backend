
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Deactivate;
using Charbagh.Application.BookItems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.BookItems.Deactivate
{
    public sealed class DeactivateBookItemHandler : IRequestHandler<DeactivateBookItemCommand, BookItemDto>
    {
        private readonly IBookItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateBookItemHandler(IBookItemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookItemDto> Handle(DeactivateBookItemCommand request, CancellationToken cancellationToken)
        {
            var bookItem = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (bookItem == null)
            {
                throw new BookItemNotFoundException(request.Id);
            }

            bookItem.Deactivate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return bookItem.ToDto();
        }
    }

}
