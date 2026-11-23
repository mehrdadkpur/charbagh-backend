
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Parents.Common;
using Charbagh.Domain.Entities.People;
using MediatR;

namespace Charbagh.Application.Parents.Create
{
    public sealed class CreateParentHandler : IRequestHandler<CreateParentCommand , ParentDto>
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateParentHandler(IParentRepository parentRepository, IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParentDto> Handle(CreateParentCommand request , CancellationToken cancellationToken)
        {
            var existUser = await _userRepository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (!existUser)
            {
                throw new ParentNotFoundException();
            }

            var existparent = await _parentRepository.ExistsByUserIdAsync(request.UserId , cancellationToken);

            if (existparent)
            {
                throw new UserAlreadyParentException(request.UserId);
            }

            var parent = Parent.Create(request.UserId);

            await _parentRepository.AddAsync(parent , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return parent.ToDto();

        }
    }
}
