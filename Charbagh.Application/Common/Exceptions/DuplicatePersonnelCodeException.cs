namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicatePersonnelCodeException : Exception
{
    public DuplicatePersonnelCodeException(string? PersonnelCode)
        : base($"The Staff With Code '{PersonnelCode}' already exists.")
    {
    }
}