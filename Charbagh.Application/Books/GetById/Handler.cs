
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Bookitems.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Bookitems.GetById
{
    public sealed class GetBookByIdHandler: IRequestHandler<GetBookByIdQuery , BookDto>
    {
        private readonly IBookRepository _repository;

        public GetBookByIdHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task <BookDto> Handle (GetBookByIdQuery request , CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(book == null)
            {
                throw new BookNotFoundException(request.Id);
            }

            return book.ToDto();
        }
    }
}
