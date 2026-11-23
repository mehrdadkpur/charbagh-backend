using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetAll
{
    public sealed class GetAllUserBranchesHandler : IRequestHandler<GetAllUserBranchesQuery ,IReadOnlyList< UserBranchDto>>
    {
        private readonly IUserBranchRepository _repository;

        public GetAllUserBranchesHandler(IUserBranchRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchDto>> Handle (GetAllUserBranchesQuery request , CancellationToken cancellationToken)
        {
            var userBranches = await _repository.GetAllAsync (cancellationToken);

            return userBranches.Select(x => x.ToDto()).ToList();

        }
    }
}
