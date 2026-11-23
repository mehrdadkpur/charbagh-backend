using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.GetAll
{
    public sealed class GetAllEvaluationsHandler: IRequestHandler<GetAllEvaluationsQuery,IReadOnlyList< EvaluationDto>>
    {
        private readonly IEvaluationRepository _repository;

        public GetAllEvaluationsHandler(IEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task <IReadOnlyList< EvaluationDto>> Handle (GetAllEvaluationsQuery request , CancellationToken cancellationToken)
        {
            var evaluations = await _repository.GetAllAsync(cancellationToken);

            return evaluations.Select(x=> x.ToDto()).ToList();
        }
    }
}
