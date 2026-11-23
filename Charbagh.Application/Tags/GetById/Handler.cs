
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.GetById
{
    public sealed class GetTagbyIdHandler : IRequestHandler<GetTagByIdQuery , TagDto>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagbyIdHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.Id , cancellationToken);

            if(tag == null)
            {
                throw new TagNotFoundException(request.Id);
            }

            return tag.ToDto();
        }
    }
}
