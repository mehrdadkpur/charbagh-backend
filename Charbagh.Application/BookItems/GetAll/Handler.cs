using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.GetAll
{
    public sealed class GetAllBookItemsHandler : IRequestHandler<GetallBookItemsQuery,IReadOnlyList< BookItemDto>>
    {
        private readonly IBookItemRepository _repository;

        public GetAllBookItemsHandler(IBookItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< BookItemDto>> Handle(GetallBookItemsQuery request, CancellationToken cancellationToken)
        {
            var bookItem = await _repository.GetAllAsync(cancellationToken);

            return bookItem.Select(x => x.ToDto()).ToList();
        }
    }

}
