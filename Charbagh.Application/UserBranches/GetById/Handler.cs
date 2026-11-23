using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetById
{
    public sealed class GetUserBranchByIdHandler : IRequestHandler<GetUserBranchByIdQuery , UserBranchDto>
    {
        private readonly IUserBranchRepository _repository;

        public GetUserBranchByIdHandler(IUserBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task <UserBranchDto> Handle(GetUserBranchByIdQuery request , CancellationToken cancellationToken)
        {
            var userBranch = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (userBranch == null)
            {
                throw new UserBranchNotFoundException(request.Id);
            }

            return userBranch.ToDto();
        }
    }
}
