
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetNotComplete
{
    public sealed record SetNotCompletePracticeCompletionStatusEvaluationCommand(Guid Id) : IRequest<EvaluationDto>;
}
