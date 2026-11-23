namespace Charbagh.Application.Common.Exceptions;

public sealed class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(Guid Id)
        : base($"This Category {Id} Not Found")
    {
    }
}