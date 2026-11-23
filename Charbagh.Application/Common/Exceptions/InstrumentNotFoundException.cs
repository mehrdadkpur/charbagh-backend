namespace Charbagh.Application.Common.Exceptions;

public sealed class InstrumentNotFoundException : Exception
{
    public InstrumentNotFoundException(Guid id)
        : base($"Instrument with id '{id}' was not found.")
    {
    }
}