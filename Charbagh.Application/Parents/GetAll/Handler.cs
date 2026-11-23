
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Parents.Common;
using MediatR;

namespace Charbagh.Application.Parents.GetAll
{
    public class GetAllParentsHandler : IRequestHandler<GetAllParentsQuery , IReadOnlyList<ParentDto>> 
    {
        private readonly IParentRepository _parentRepository;

        public GetAllParentsHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<IReadOnlyList<ParentDto>> Handle (GetAllParentsQuery query , CancellationToken cancellationToken)
        {
            var parents = await _parentRepository.GetAllAsync(cancellationToken);

            return parents
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
