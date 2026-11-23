namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateInstrumentException : Exception
{
    public DuplicateInstrumentException(string Name)
        : base($"Branch Name '{Name}' already exists.")
    {
    }
}