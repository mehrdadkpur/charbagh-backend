namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateActiveServicePriceException : Exception
{
    public DuplicateActiveServicePriceException(Guid EducationServiceId)
        : base($"The Service Price with Education Service Id '{EducationServiceId}' already Exist.")
    {
    }
}