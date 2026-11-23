
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.GetAll
{
    public sealed class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery ,IReadOnlyList< TagDto>>
    {
        private readonly ITagRepository _repository;

        public GetAllTagsHandler(ITagRepository repository) 
        {
            _repository = repository; 
        }

        public async Task<IReadOnlyList< TagDto>> Handle(GetAllTagsQuery request , CancellationToken cancellationToken)
        {
            var tags = await _repository.GetAllAsync(cancellationToken);

            return tags.Select(x => x.ToDto()).ToList();
        }
    }
}
