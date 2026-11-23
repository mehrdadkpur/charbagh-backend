
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetNotAssigned
{
    public sealed record SetNotAssignedPracticeCompletionStatusEvaluationCommand(Guid Id) : IRequest<EvaluationDto>;
}
