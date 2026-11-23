using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Block
{
    public sealed class BlockParentHandler : IRequestHandler<BlockParentCommand, ParentDto>
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlockParentHandler(IParentRepository parentRepository, IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParentDto> Handle(BlockParentCommand request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (parent == null)
            {
                throw new ParentNotFoundException();
            }

            parent.Block();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return parent.ToDto();
        }
    }
}

