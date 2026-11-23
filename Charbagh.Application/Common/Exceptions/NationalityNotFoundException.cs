namespace Charbagh.Application.Common.Exceptions;

public sealed class NationalityNotFoundException : Exception
{
    public NationalityNotFoundException(Guid NationalityId)
        : base($"This Nationality With {NationalityId} ID Not Found")
    {
    }
}