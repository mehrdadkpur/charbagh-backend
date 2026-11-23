namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateMobileException : Exception
{
    public DuplicateMobileException(string mobile)
        : base($"The Mobile Number '{mobile}' already exists.")
    {
    }
}