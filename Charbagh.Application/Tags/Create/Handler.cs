using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Tags.Common;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Tags.Create
{
    public sealed class CreateTagHandler : IRequestHandler<CreateTagCommand , TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<TagDto> Handle(CreateTagCommand request , CancellationToken cancellationToken)
        {

            var existsName = await _tagRepository.ExistsTagNameAsync(request.Name, cancellationToken);

            if (existsName)
            {
                throw new DuplicateTagNameException(request.Name);
            }

            var tag = Tag.Create(request.Name);

            await _tagRepository.AddAsync(tag , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag.ToDto();
        }
    }
}
