using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.GetById
{
    public sealed class GetEvaluationByIdHandler : IRequestHandler<GetEvaluationByIdQuery , EvaluationDto>
    {
        private readonly IEvaluationRepository _repository;

        public GetEvaluationByIdHandler(IEvaluationRepository repository)
        {
            _repository = repository; 
        }

        public async Task<EvaluationDto> Handle (GetEvaluationByIdQuery request , CancellationToken cancellationToken)
        {
            var evaluation = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }

            return evaluation.ToDto();
        }
    }
}
