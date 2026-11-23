using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.Activate
{
    public sealed class ActivateParentHandler:IRequestHandler<ActivateParentCommand , ParentDto>
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateParentHandler(IParentRepository parentRepository, IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParentDto> Handle(ActivateParentCommand request , CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetByIdAsync(request.Id , cancellationToken);

            if (parent == null)
            {
                throw new ParentNotFoundException();
            }

            parent.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return parent.ToDto();
        }
    }
}
