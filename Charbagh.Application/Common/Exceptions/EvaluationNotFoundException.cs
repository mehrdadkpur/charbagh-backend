namespace Charbagh.Application.Common.Exceptions;

public sealed class EvaluationNotFoundException : Exception
{
    public EvaluationNotFoundException(Guid Id)
        : base($"This Evaluation {Id} Not Found")
    {
    }
}