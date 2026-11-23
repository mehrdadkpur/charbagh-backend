
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.BookItems.Activate
{
    public sealed class ActivateBookItemHandler : IRequestHandler<ActivateBookItemCommand , BookItemDto>
    {
        private readonly IBookItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateBookItemHandler(IBookItemRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task <BookItemDto> Handle (ActivateBookItemCommand request , CancellationToken cancellationToken)
        {
            var bookItem = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (bookItem == null)
            {
                throw new BookItemNotFoundException(request.Id);
            }

            bookItem.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return bookItem.ToDto();
        }
    }
}
