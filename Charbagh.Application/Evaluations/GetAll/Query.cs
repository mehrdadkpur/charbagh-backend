
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.GetAll
{
    public sealed record GetAllEvaluationsQuery(): IRequest<IReadOnlyList< EvaluationDto>>;
}
