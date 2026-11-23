namespace Charbagh.Application.Common.Exceptions;

public sealed class ServicePriceNotFoundException : Exception
{
    public ServicePriceNotFoundException(Guid Id)
        : base($"This Service Price {Id} Not Found")
    {
    }
}