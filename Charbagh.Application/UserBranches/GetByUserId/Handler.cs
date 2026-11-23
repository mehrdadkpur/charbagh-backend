using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetByUserId
{
    public sealed class GetUserBranchesByUserIdHandler : IRequestHandler<GetUserBranchesByUserIdQuery ,IReadOnlyList< UserBranchDto>>
    {
        private readonly IUserBranchRepository _repository;

        public GetUserBranchesByUserIdHandler(IUserBranchRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchDto>> Handle (GetUserBranchesByUserIdQuery request , CancellationToken cancellationToken)
        {
            var userBranches = await _repository.GetByUserIdAsync (request.UserId,cancellationToken);

            return userBranches.Select(x => x.ToDto()).ToList();

        }
    }
}
