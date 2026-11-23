
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.GetById
{
    public sealed class GetParentByIdHandler : IRequestHandler<GetParentByIdQuery , ParentDto>
    {
        private readonly IParentRepository _parentRepository;

        public GetParentByIdHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<ParentDto> Handle (GetParentByIdQuery request , CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetByIdAsync(request.Id , cancellationToken);

            if (parent == null)
            {
                throw new ParentNotFoundException();
            }
             return parent.ToDto();
        }
    }
}
