namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateTeacherCodeException : Exception
{
    public DuplicateTeacherCodeException(string? teacherCode)
        : base($"The Teacher With Code '{teacherCode}' already exists.")
    {
    }
}