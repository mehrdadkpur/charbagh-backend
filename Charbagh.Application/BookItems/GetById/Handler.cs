
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.BookItems.GetById
{
    public sealed class GetBookItemByIdHandler : IRequestHandler<GetBookItemByIdQuery , BookItemDto>
    {
        private readonly IBookItemRepository _repository;

        public GetBookItemByIdHandler(IBookItemRepository repository)
        {
            _repository = repository;
        }

        public async Task <BookItemDto> Handle (GetBookItemByIdQuery request , CancellationToken cancellationToken)
        {
            var bookItem = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if(bookItem == null)
            {
                throw new BookItemNotFoundException(request.Id);
            }

            return bookItem.ToDto();
        }
    }
}
