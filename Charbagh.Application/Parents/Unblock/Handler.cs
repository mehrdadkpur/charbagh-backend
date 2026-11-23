using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Parents.Block;
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Unblock
{
    public sealed class UnblockParentHandler : IRequestHandler<UnblockParentCommand, ParentDto>
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockParentHandler(IParentRepository parentRepository, IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParentDto> Handle(UnblockParentCommand request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (parent == null)
            {
                throw new ParentNotFoundException();
            }

            parent.Unblock();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return parent.ToDto();
        }
    }
}
