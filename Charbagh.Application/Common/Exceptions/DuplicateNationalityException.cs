namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateNationalityException : Exception
{
    public DuplicateNationalityException(string Code)
        : base($"The Nationality '{Code}' already exists.")
    {
    }
}