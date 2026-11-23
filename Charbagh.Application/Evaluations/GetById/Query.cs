
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.GetById
{
    public sealed record GetEvaluationByIdQuery(Guid Id) : IRequest<EvaluationDto>;
}
