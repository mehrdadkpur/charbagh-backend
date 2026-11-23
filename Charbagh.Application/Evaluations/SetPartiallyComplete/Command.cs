
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetPartiallyComplete
{
    public sealed record SetPartiallyCompletePracticeCompletionStatusEvaluationCommand(Guid Id) : IRequest<EvaluationDto>;
}
