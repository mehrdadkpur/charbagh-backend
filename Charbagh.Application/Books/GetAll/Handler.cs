using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.GetAll
{
    public sealed class GetAllBooksHandler:IRequestHandler<GetAllBooksQuery ,IReadOnlyList< BookDto>>
    {
        private readonly IBookRepository _repository;

        public GetAllBooksHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<BookDto>> Handle (GetAllBooksQuery request , CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync (cancellationToken);

            return books.Select(x => x.ToDto()).ToList();
        }
    }
}
