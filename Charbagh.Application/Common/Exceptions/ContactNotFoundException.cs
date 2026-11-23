namespace Charbagh.Application.Common.Exceptions;

public sealed class ContactNotFoundException : Exception
{
    public ContactNotFoundException(Guid Id)
        : base($"This Contact {Id} Not Found")
    {
    }
}