namespace Charbagh.Application.Common.Exceptions;

public sealed class TeachingPlanNotFoundException : Exception
{
    public TeachingPlanNotFoundException(Guid teachingPlanId)
        : base($"This Teaching plan '{teachingPlanId}' Not Found")
    {
    }
}