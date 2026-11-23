namespace Charbagh.Application.Common.Exceptions;

public sealed class TagNotFoundException : Exception
{
    public TagNotFoundException(Guid Id)
        : base($"This Tag {Id} Not Found")
    {
    }
}