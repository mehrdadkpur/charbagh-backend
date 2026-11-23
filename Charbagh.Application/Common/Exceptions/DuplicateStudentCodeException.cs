namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateStudentCodeException : Exception
{
    public DuplicateStudentCodeException(string? StudentCode)
        : base($"The Student With Code '{StudentCode}' already exists.")
    {
    }
}