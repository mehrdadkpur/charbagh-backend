namespace Charbagh.Application.Common.Exceptions;

public sealed class EducationServiceNotFoundException : Exception
{
    public EducationServiceNotFoundException(Guid Id)
        : base($"This Education Service {Id} Not Found")
    {
    }
}