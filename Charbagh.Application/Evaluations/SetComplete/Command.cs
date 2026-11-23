using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetComplete
{
    public sealed record SetCompletePracticeCompletionStatusEvaluationCommand(Guid Id) : IRequest<EvaluationDto>;
}
