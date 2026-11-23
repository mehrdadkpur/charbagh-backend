using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetByBranchId
{
    public sealed class GetUserBranchesByBranchIdHandler : IRequestHandler<GetUserBranchesByBranchIdQuery ,IReadOnlyList< UserBranchDto>>
    {
        private readonly IUserBranchRepository _repository;

        public GetUserBranchesByBranchIdHandler(IUserBranchRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchDto>> Handle (GetUserBranchesByBranchIdQuery request , CancellationToken cancellationToken)
        {
            var userBranches = await _repository.GetByBranchIdAsync (request.BranchId,cancellationToken);

            return userBranches.Select(x => x.ToDto()).ToList();

        }
    }
}
