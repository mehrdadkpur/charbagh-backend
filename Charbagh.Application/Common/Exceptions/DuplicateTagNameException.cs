namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateTagNameException : Exception
{
    public DuplicateTagNameException(string TagName)
        : base($"The Tag '{TagName}' already exists.")
    {
    }
}